using AssociationMembership.Application.Features.Memberships.Commands.CreateMembership;
using AssociationMembership.Application.Features.Memberships.Commands.DeleteMembership;
using AssociationMembership.Application.Features.Memberships.Commands.UpdateMembership;
using AssociationMembership.Application.Features.Memberships.Commands.UpdateMembershipStatus;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Application.Features.Memberships.Queries.GetMembershipById;
using AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByGroup;
using AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByStatus;
using AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsOverduePayment;
using AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsWaitingPayment;
using AssociationMembership.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssociationMembership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new membership application (Public endpoint)
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMembership([FromBody] CreateMembershipCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(
                nameof(GetMembershipById),
                new { id = result.Value.Id },
                result.Value);
        }

        /// <summary>
        /// Get membership by ID
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMembershipById(int id)
        {
            var query = new GetMembershipByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Get memberships by tenant group
        /// </summary>
        [HttpGet("group/{tenantGroupId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembershipsByGroup(
            int tenantGroupId,
            [FromQuery] MembershipStatus? status = null)
        {
            var query = new GetMembershipsByGroupQuery
            {
                TenantGroupId = tenantGroupId,
                Status = status
            };

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Update membership information (only for Pending/PreApproved status)
        /// </summary>
        [HttpPut("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMembership(int id, [FromBody] UpdateMembershipDto dto)
        {
            var command = new UpdateMembershipCommand
            {
                Id = id,
                Membership = dto
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Update membership status (Admin only)
        /// </summary>
        [HttpPatch("{id}/status")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMembershipStatus(
            int id,
            [FromBody] UpdateMembershipStatusCommand command)
        {
            if (id != command.MembershipId)
                return BadRequest(new { error = "ID mismatch" });

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Delete membership (only Pending/Rejected)
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            var command = new DeleteMembershipCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }


        /// <summary>
        /// Get memberships waiting for payment (PreApproved status, no payment received)
        /// </summary>
        [HttpGet("waiting-payment")]
        [ProducesResponseType(typeof(List<MembershipListDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembershipsWaitingPayment()
        {
            var query = new GetMembershipsWaitingPaymentQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Get memberships with overdue payments (PreApproved for more than 30 days without payment)
        /// </summary>
        [HttpGet("overdue-payments")]
        [ProducesResponseType(typeof(List<MembershipListDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembershipsOverduePayment()
        {
            var query = new GetMembershipsOverduePaymentQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        /// <summary>
        /// Get memberships by status
        /// </summary>
        [HttpGet("by-status/{status}")]
        [ProducesResponseType(typeof(List<MembershipListDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembershipsByStatus(MembershipStatus status)
        {
            var query = new GetMembershipsByStatusQuery { Status = status };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}
