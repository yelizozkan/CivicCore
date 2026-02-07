using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Domain.Common.Enums;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AssociationMembership.Domain.Models;

namespace AssociationMembership.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                return await next();

            }


            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            

            if (failures.Any())
            {

                var validationErrors = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(f => f.ErrorMessage).ToArray()
                    );

                // Create single Error object with all validation errors
                var error = Error.Validation(
                    ErrorCode.ValidationFailed,
                    "One or more validation errors occurred",
                    validationErrors);

                // Return failure result
                return CreateFailureResult(error);


                //var errors = failures.Select(x => x.ErrorMessage).ToList();

                //// Create Result.Failure with errors
                //var resultType = typeof(TResponse);

                //if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                //{
                //    var dataType = resultType.GetGenericArguments()[0];
                //    var method = typeof(Result)
                //        .GetMethods()
                //        .First(m => m.Name == "Failure" && m.IsGenericMethodDefinition && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(List<string>))
                //        .MakeGenericMethod(dataType);

                //    return (TResponse)method.Invoke(null, new object[] { errors });
                //}
                //else
                //{
                //    var method = typeof(Result)
                //        .GetMethods()
                //        .First(m => m.Name == "Failure" && !m.IsGenericMethodDefinition && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(List<string>));

                //    return (TResponse)method.Invoke(null, new object[] { errors });
            }

            return await next();
        }

        private TResponse CreateFailureResult(Error error)
        {
            var resultType = typeof(TResponse);

            try
            {
                if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                {
                    // Result<T>.Failure(Error[] errors)
                    var dataType = resultType.GetGenericArguments()[0];
                    var failureMethod = typeof(Result<>)
                        .MakeGenericType(dataType)
                        .GetMethod("Failure", new[] { typeof(Error[]) });

                    if (failureMethod != null)
                    {
                        return (TResponse)failureMethod.Invoke(null, new object[] { new[] { error } });
                    }
                }
                else if (resultType == typeof(Result))
                {
                    // Result.Failure(Error[] errors)
                    var failureMethod = typeof(Result)
                        .GetMethod("Failure", new[] { typeof(Error[]) });

                    if (failureMethod != null)
                    {
                        return (TResponse)failureMethod.Invoke(null, new object[] { new[] { error } });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create failure result for type {resultType.Name}", ex);
            }

            throw new InvalidOperationException($"Unable to create failure result for type {resultType.Name}");
        }
    }
            
}
