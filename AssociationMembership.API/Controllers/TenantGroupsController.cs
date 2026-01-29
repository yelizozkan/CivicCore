using AssociationMembership.Application.Features.TenantGroup.Commands.CreateTenantGroup;
using AssociationMembership.Application.Features.TenantGroup.Commands.DeleteTenantGroup;
using AssociationMembership.Application.Features.TenantGroup.Commands.UpdateTenantGroup;
using AssociationMembership.Application.Features.TenantGroup.Dtos;
using AssociationMembership.Application.Features.TenantGroup.Queries.GetAllTenantGroups;
using AssociationMembership.Application.Features.TenantGroup.Queries.GetTenantGroupById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssociationMembership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class TenantGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all tenant groups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<TenantGroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTenantGroups()
        {
            var query = new GetAllTenantGroupsQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Get tenant group by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TenantGroupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTenantGroupById(int id)
        {
            var query = new GetTenantGroupByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Create a new tenant group
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TenantGroupDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTenantGroup([FromBody] CreateTenantGroupDto dto)
        {
            var command = new CreateTenantGroupCommand
            {
                TenantId = dto.TenantId,
                Name = dto.Name,
                Description = dto.Description
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(
                nameof(GetTenantGroupById),
                new { id = result.Value.Id },
                result.Value);
        }

        /// <summary>
        /// Update an existing tenant group
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TenantGroupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTenantGroup(int id, [FromBody] UpdateTenantGroupDto dto)
        {
            var command = new UpdateTenantGroupCommand
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Delete a tenant group (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTenantGroup(int id)
        {
            var command = new DeleteTenantGroupCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }
    }
}
