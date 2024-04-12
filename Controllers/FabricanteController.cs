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
    public class FabricanteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FabricanteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fabricante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fabricante>>> GetFabricantes()
        {
          if (_context.Fabricantes == null)
          {
              return NotFound();
          }
            return await _context.Fabricantes.ToListAsync();
        }

        // GET: api/Fabricante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fabricante>> GetFabricante(int id)
        {
          if (_context.Fabricantes == null)
          {
              return NotFound();
          }
            var fabricante = await _context.Fabricantes.FindAsync(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return fabricante;
        }

        // PUT: api/Fabricante/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabricante(int id, Fabricante fabricante)
        {
            if (id != fabricante.CodFab)
            {
                return BadRequest();
            }

            _context.Entry(fabricante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricanteExists(id))
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

        // POST: api/Fabricante
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fabricante>> PostFabricante(Fabricante fabricante)
        {
          if (_context.Fabricantes == null)
          {
              return Problem("Entity set 'AppDbContext.Fabricantes'  is null.");
          }
            _context.Fabricantes.Add(fabricante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFabricante", new { id = fabricante.CodFab }, fabricante);
        }

        // DELETE: api/Fabricante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabricante(int id)
        {
            if (_context.Fabricantes == null)
            {
                return NotFound();
            }
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                return NotFound();
            }

            _context.Fabricantes.Remove(fabricante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FabricanteExists(int id)
        {
            return (_context.Fabricantes?.Any(e => e.CodFab == id)).GetValueOrDefault();
        }
    }
}
