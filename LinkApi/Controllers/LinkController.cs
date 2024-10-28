using LinkExpiry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LinkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private static Dictionary<Guid, Link> links = new Dictionary<Guid, Link>();

        //private readonly IMemoryCache _cache;
        //private const string LinkCacheKey = "SecretLinks_";

        //public SecretController(IMemoryCache cache)
        //{
        //    _cache = cache;
        //}
    
        // Endpoint to access the supersecret page
        [HttpGet("/supersecret/{id}")]
        public IActionResult AccessSecret(Guid id)
        {
            if (!links.ContainsKey(id))
                return NotFound("There are no secrets here.");

            var link = links[id];

            //// Check if the link has been accessed before
            //if (link.IsAccessed)
            //    return Ok("There are no secrets here.");
            //var cacheKey = LinkCacheKey + request.Username;
            //_cache.Set(cacheKey, userSecretLink, new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = userSecretLink.Expiration.HasValue ? TimeSpan.FromMinutes(request.ExpirationMinutes.Value) : TimeSpan.FromHours(24) // Cache it for 24 hours if no expiration.
            //});

            // Check if the link has expired
            if (link.Expiration.HasValue && link.Expiration < DateTime.UtcNow)
                return Ok("There are no secrets here. The link has expired.");

            // Check if the link has been accessed more than the allowed number of times
            if (link.CurrentAccessCount >= link.MaxAccessCount)
                return Ok("There are no secrets here. The link has been accessed too many times.");

            // Mark the link as accessed by incrementing the access count
            link.CurrentAccessCount++;


            // Mark the link as accessed
           // link.IsAccessed = true;
            return Ok($"You have found the secret, {link.Username}!");
        }

        [HttpPost("generate")]
        public IActionResult GenerateLink([FromBody] LinkRequest request, [FromQuery] int? expirationMinutes, [FromQuery] int maxClicks = 1)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Set the expiration if provided, otherwise it's null (no expiration).
            DateTime? expiration = expirationMinutes.HasValue ? DateTime.UtcNow.AddMinutes(expirationMinutes.Value) : null;

            // Create a new link with max access count
            var link = new Link
            {
                Username = request.Username,
                Expiration = expiration,
                MaxAccessCount = maxClicks
            };
           
            links.Add(link.Id, link);

            var linkUrl = $"{Request.Scheme}://{Request.Host}/supersecret/{link.Id}";
            return Ok(new { linkUrl });
        }



    }
}
