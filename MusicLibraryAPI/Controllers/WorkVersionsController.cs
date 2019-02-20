using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Models;

namespace MusicLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkVersionsController : ControllerBase
    {
        private readonly musiclibraryContext _context;

        public WorkVersionsController(musiclibraryContext context)
        {
            _context = context;
        }

        // GET: api/WorkVersions
        [HttpGet]
        public IEnumerable<WorkVersion> GetWorkVersion(int? workid)
        {
            var workversion = from wv in _context.WorkVersion select wv;
            if (workid != null)
            {
                workversion = workversion.Where(wv => wv.WorkId == workid);
            }
            return workversion;
        }

        // GET: api/WorkVersions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workVersion = await _context.WorkVersion.FindAsync(id);

            if (workVersion == null)
            {
                return NotFound();
            }

            return Ok(workVersion);
        }

        // PUT: api/WorkVersions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkVersion([FromRoute] int id, [FromBody] WorkVersion workVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workVersion.WorkVersionId)
            {
                return BadRequest();
            }

            _context.Entry(workVersion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkVersionExists(id))
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

        // POST: api/WorkVersions
        [HttpPost]
        public async Task<IActionResult> PostWorkVersion([FromBody] WorkVersion workVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkVersion.Add(workVersion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkVersion", new { id = workVersion.WorkVersionId }, workVersion);
        }

        // DELETE: api/WorkVersions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workVersion = await _context.WorkVersion.FindAsync(id);
            if (workVersion == null)
            {
                return NotFound();
            }

            _context.WorkVersion.Remove(workVersion);
            await _context.SaveChangesAsync();

            return Ok(workVersion);
        }

        private bool WorkVersionExists(int id)
        {
            return _context.WorkVersion.Any(e => e.WorkVersionId == id);
        }
    }
}