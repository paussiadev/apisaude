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
    public class MedicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Medico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
          if (_context.Medicos == null)
          {
              return NotFound();
          }
            return await _context.Medicos.ToListAsync();
        }

        // GET: api/Medico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
          if (_context.Medicos == null)
          {
              return NotFound();
          }
            var medico = await _context.Medicos.FindAsync(id);

            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        // PUT: api/Medico/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedico(int id, Medico medico)
        {
            if (id != medico.CodMedico)
            {
                return BadRequest();
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(id))
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

        // POST: api/Medico
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
          if (_context.Medicos == null)
          {
              return Problem("Entity set 'AppDbContext.Medicos'  is null.");
          }
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedico", new { id = medico.CodMedico }, medico);
        }

        // DELETE: api/Medico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            if (_context.Medicos == null)
            {
                return NotFound();
            }
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicoExists(int id)
        {
            return (_context.Medicos?.Any(e => e.CodMedico == id)).GetValueOrDefault();
        }
    }
}
