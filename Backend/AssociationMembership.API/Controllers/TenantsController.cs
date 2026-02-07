using AssociationMembership.Application.Features.Tenant.Commands.CreateTenant;
using AssociationMembership.Application.Features.Tenant.Commands.DeleteTenant;
using AssociationMembership.Application.Features.Tenant.Commands.UpdateTenant;
using AssociationMembership.Application.Features.Tenant.Dtos;
using AssociationMembership.Application.Features.Tenant.Queries.GetAllTenants;
using AssociationMembership.Application.Features.Tenant.Queries.GetTenantById;
using AssociationMembership.Application.Features.Tenant.Queries.GetTenantWithGroups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssociationMembership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all tenants
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<TenantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTenants()
        {
            var query = new GetAllTenantsQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Get tenant by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TenantDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var query = new GetTenantByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Get tenant with groups
        /// </summary>
        [HttpGet("{id}/with-groups")]
        [ProducesResponseType(typeof(TenantWithGroupsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTenantWithGroups(int id)
        {
            var query = new GetTenantWithGroupsQuery { Id = id };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Create a new tenant
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TenantDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantDto dto)
        {
            var command = new CreateTenantCommand
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                LogoUrl = dto.LogoUrl
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(
                nameof(GetTenantById),
                new { id = result.Value.Id },
                result.Value);
        }

        /// <summary>
        /// Update an existing tenant
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TenantDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTenant(int id, [FromBody] UpdateTenantDto dto)
        {
            var command = new UpdateTenantCommand
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                LogoUrl = dto.LogoUrl,
                IsActive = dto.IsActive
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Delete a tenant (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var command = new DeleteTenantCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }
    }
}
