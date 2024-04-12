using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisaude.Context;
using apisaude.Models;

namespace apisaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Entrega
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
          if (_context.Entregas == null)
          {
              return NotFound();
          }
            return await _context.Entregas.ToListAsync();
        }

        // GET: api/Entrega/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
          if (_context.Entregas == null)
          {
              return NotFound();
          }
            var entrega = await _context.Entregas.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return entrega;
        }

        // PUT: api/Entrega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.CodEntrega)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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

        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
          if (_context.Entregas == null)
          {
              return Problem("Entity set 'AppDbContext.Entregas'  is null.");
          }
            _context.Entregas.Add(entrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrega", new { id = entrega.CodEntrega }, entrega);
        }

        // DELETE: api/Entrega/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return (_context.Entregas?.Any(e => e.CodEntrega == id)).GetValueOrDefault();
        }
    }
}
