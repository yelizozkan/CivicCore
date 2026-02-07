using AssociationMembership.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AssociationMembership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublicController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Resolve slug to get tenant/group information for registration
        /// </summary>
        /// <param name="slug">URL slug (e.g., busader-genc)</param>
        [HttpGet("resolve/{slug}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResolveSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return BadRequest(new { message = "Slug is required" });

            slug = slug.ToLowerInvariant().Trim();


            var group = await _context.TenantGroups
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(x => x.Slug == slug && !x.IsDeleted);

            if (group != null)
            {
                return Ok(new SlugResolveResponse
                {
                    Type = "group",
                    TenantId = group.TenantId,
                    TenantGroupId = group.Id,
                    Name = group.Name,
                    TenantName = group.Tenant?.Name,
                    Slug = group.Slug
                });
            }


            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(x => x.Slug == slug && !x.IsDeleted && x.IsActive);

            if (tenant != null)
            {
                return Ok(new SlugResolveResponse
                {
                    Type = "tenant",
                    TenantId = tenant.Id,
                    TenantGroupId = null,
                    Name = tenant.Name,
                    TenantName = tenant.Name,
                    Slug = tenant.Slug
                });
            }


            return NotFound(new { message = "Geçersiz kayıt linki" });
        }
    }

    public class SlugResolveResponse
    {
        public string Type { get; set; }  // "tenant" or "group"
        public int TenantId { get; set; }
        public int? TenantGroupId { get; set; }
        public string Name { get; set; }
        public string TenantName { get; set; }
        public string Slug { get; set; }
    }
}
