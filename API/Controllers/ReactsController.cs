using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactsController : ControllerBase
    {
        private readonly EntitiesContext _context;

        public ReactsController(EntitiesContext context)
        {
            _context = context;
        }

        // GET: api/Reacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<React>>> GetReacts()
        {
            if (_context.Reacts == null)
            {
                return NotFound();
            }
            return await _context.Reacts.ToListAsync();
        }

        // GET: api/Reacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<React>> GetReact(int id)
        {
            if (_context.Reacts == null)
            {
                return NotFound();
            }
            var react = await _context.Reacts.FindAsync(id);

            if (react == null)
            {
                return NotFound();
            }

            return react;
        }

        // PUT: api/Reacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReact(int id, React react)
        {
            if (id != react.ID)
            {
                return BadRequest();
            }

            _context.Entry(react).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<React>> PostReact(React react)
        {
            if (_context.Reacts == null)
            {
                return Problem("Entity set 'EntitiesContext.Reacts'  is null.");
            }
            _context.Reacts.Add(react);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReact", new { id = react.ID }, react);
        }

        // DELETE: api/Reacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReact(int id)
        {
            if (_context.Reacts == null)
            {
                return NotFound();
            }
            var react = await _context.Reacts.FindAsync(id);
            if (react == null)
            {
                return NotFound();
            }

            _context.Reacts.Remove(react);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReactExists(int id)
        {
            return (_context.Reacts?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
