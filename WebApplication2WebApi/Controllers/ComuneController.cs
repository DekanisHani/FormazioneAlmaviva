using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2WebApi.Models.DB;

namespace WebApplication2WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComuneController : ControllerBase
    {
        private readonly masterContext _context;

        public ComuneController(masterContext context)
        {
            _context = context;
        }

        // GET: api/Comune
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comune>>> GetComune()
        {
            return await _context.Comune.ToListAsync();
        }

        // GET: api/Comune/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comune>> GetComune(int id)
        {
            var comune = await _context.Comune.FindAsync(id);

            if (comune == null)
            {
                return NotFound();
            }

            return comune;
        }

        // PUT: api/Comune/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComune(int id, Comune comune)
        {
            if (id != comune.Id)
            {
                return BadRequest();
            }

            _context.Entry(comune).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComuneExists(id))
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

        // POST: api/Comune
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comune>> PostComune(Comune comune)
        {
            _context.Comune.Add(comune);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComune", new { id = comune.Id }, comune);
        }

        // DELETE: api/Comune/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComune(int id)
        {
            var comune = await _context.Comune.FindAsync(id);
            if (comune == null)
            {
                return NotFound();
            }

            _context.Comune.Remove(comune);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComuneExists(int id)
        {
            return _context.Comune.Any(e => e.Id == id);
        }
    }
}
