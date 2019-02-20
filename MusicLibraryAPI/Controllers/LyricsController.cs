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
    public class LyricsController : ControllerBase
    {
        private readonly musiclibraryContext _context;

        public LyricsController(musiclibraryContext context)
        {
            _context = context;
        }

        // GET: api/Lyrics
        [HttpGet]
        public IEnumerable<Lyric> GetLyric()
        {
            return _context.Lyric;
        }

        // GET: api/Lyrics/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLyric([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lyric = await _context.Lyric.FindAsync(id);

            if (lyric == null)
            {
                return NotFound();
            }

            return Ok(lyric);
        }

        // PUT: api/Lyrics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLyric([FromRoute] int id, [FromBody] Lyric lyric)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lyric.LyricId)
            {
                return BadRequest();
            }

            _context.Entry(lyric).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LyricExists(id))
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

        // POST: api/Lyrics
        [HttpPost]
        public async Task<IActionResult> PostLyric([FromBody] Lyric lyric)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Lyric.Add(lyric);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLyric", new { id = lyric.LyricId }, lyric);
        }

        // DELETE: api/Lyrics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLyric([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lyric = await _context.Lyric.FindAsync(id);
            if (lyric == null)
            {
                return NotFound();
            }

            _context.Lyric.Remove(lyric);
            await _context.SaveChangesAsync();

            return Ok(lyric);
        }

        private bool LyricExists(int id)
        {
            return _context.Lyric.Any(e => e.LyricId == id);
        }
    }
}