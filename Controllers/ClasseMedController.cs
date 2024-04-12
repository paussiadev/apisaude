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
    public class ClasseMedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClasseMedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClasseMed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClasseMed>>> GetClasseMeds()
        {
          if (_context.ClasseMeds == null)
          {
              return NotFound();
          }
            return await _context.ClasseMeds.ToListAsync();
        }

        // GET: api/ClasseMed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClasseMed>> GetClasseMed(int id)
        {
          if (_context.ClasseMeds == null)
          {
              return NotFound();
          }
            var classeMed = await _context.ClasseMeds.FindAsync(id);

            if (classeMed == null)
            {
                return NotFound();
            }

            return classeMed;
        }

        // PUT: api/ClasseMed/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasseMed(int id, ClasseMed classeMed)
        {
            if (id != classeMed.CodClass)
            {
                return BadRequest();
            }

            _context.Entry(classeMed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasseMedExists(id))
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

        // POST: api/ClasseMed
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClasseMed>> PostClasseMed(ClasseMed classeMed)
        {
          if (_context.ClasseMeds == null)
          {
              return Problem("Entity set 'AppDbContext.ClasseMeds'  is null.");
          }
            _context.ClasseMeds.Add(classeMed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasseMed", new { id = classeMed.CodClass }, classeMed);
        }

        // DELETE: api/ClasseMed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasseMed(int id)
        {
            if (_context.ClasseMeds == null)
            {
                return NotFound();
            }
            var classeMed = await _context.ClasseMeds.FindAsync(id);
            if (classeMed == null)
            {
                return NotFound();
            }

            _context.ClasseMeds.Remove(classeMed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClasseMedExists(int id)
        {
            return (_context.ClasseMeds?.Any(e => e.CodClass == id)).GetValueOrDefault();
        }
    }
}
