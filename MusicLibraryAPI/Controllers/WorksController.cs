using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI;

namespace MusicLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly musiclibraryContext _context;

        public WorksController(musiclibraryContext context)
        {
            _context = context;
        }

        // GET: api/Works
        [HttpGet]
        public IEnumerable<Work> GetWork()
        {
            return _context.Work;
        }

        // GET: api/Works/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWork([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var work = await _context.Work.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            return Ok(work);
        }

        // PUT: api/Works/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork([FromRoute] int id, [FromBody] Work work)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != work.WorkId)
            {
                return BadRequest();
            }

            _context.Entry(work).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(id))
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

        // POST: api/Works
        [HttpPost]
        public async Task<IActionResult> PostWork([FromBody] Work work)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Work.Add(work);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWork", new { id = work.WorkId }, work);
        }

        // DELETE: api/Works/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var work = await _context.Work.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }

            _context.Work.Remove(work);
            await _context.SaveChangesAsync();

            return Ok(work);
        }

        private bool WorkExists(int id)
        {
            return _context.Work.Any(e => e.WorkId == id);
        }
    }
}