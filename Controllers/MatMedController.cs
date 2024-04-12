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
    public class MatMedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatMedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MatMed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatMed>>> GetMatMeds()
        {
          if (_context.MatMeds == null)
          {
              return NotFound();
          }
            return await _context.MatMeds.ToListAsync();
        }

        // GET: api/MatMed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatMed>> GetMatMed(int id)
        {
          if (_context.MatMeds == null)
          {
              return NotFound();
          }
            var matMed = await _context.MatMeds.FindAsync(id);

            if (matMed == null)
            {
                return NotFound();
            }

            return matMed;
        }

        // PUT: api/MatMed/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatMed(int id, MatMed matMed)
        {
            if (id != matMed.CodMatmed)
            {
                return BadRequest();
            }

            _context.Entry(matMed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatMedExists(id))
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

        // POST: api/MatMed
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatMed>> PostMatMed(MatMed matMed)
        {
          if (_context.MatMeds == null)
          {
              return Problem("Entity set 'AppDbContext.MatMeds'  is null.");
          }
            _context.MatMeds.Add(matMed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatMed", new { id = matMed.CodMatmed }, matMed);
        }

        // DELETE: api/MatMed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatMed(int id)
        {
            if (_context.MatMeds == null)
            {
                return NotFound();
            }
            var matMed = await _context.MatMeds.FindAsync(id);
            if (matMed == null)
            {
                return NotFound();
            }

            _context.MatMeds.Remove(matMed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatMedExists(int id)
        {
            return (_context.MatMeds?.Any(e => e.CodMatmed == id)).GetValueOrDefault();
        }
    }
}
