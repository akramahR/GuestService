using GuestService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GuestService.Data.AppDbContext;

namespace GuestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly GuestContext _context;

        public RequestsController(GuestContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return request;
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(RequestDTO requestDto)
        {
            var request = new Request
            {
                GuestId = requestDto.GuestId,
                RequestType = requestDto.RequestType,
                Description = requestDto.Description,
                Status = "Pending", // Automatically set to pending
                RequestDate = DateTime.Now // Automatically set the request date
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequest), new { id = request.Id }, request);
        }

        // PUT: api/Requests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, UpdateRequestDto requestDto)
        {
            // Check if the request exists
            var existingRequest = await _context.Requests.FindAsync(id);
            if (existingRequest == null)
            {
                return NotFound();
            }

            // Update properties of the existing request with values from the DTO
            existingRequest.GuestId = requestDto.GuestId;
            existingRequest.RequestType = requestDto.RequestType;
            existingRequest.Description = requestDto.Description;
            existingRequest.Status = requestDto.Status;
            existingRequest.RequestDate = requestDto.RequestDate;

            // Mark the entity as modified
            _context.Entry(existingRequest).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the request still exists in case of a concurrency issue
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Rethrow if it's a different issue
                }
            }

            return NoContent(); // Return 204 No Content if the update was successful
        }

        // DELETE: api/Requests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
