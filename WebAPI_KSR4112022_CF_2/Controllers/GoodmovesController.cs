using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_KSR4112022_CF_2.Models;

namespace WebAPI_KSR4112022_CF_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodmovesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GoodmovesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Goodmoves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goodmove>>> GetGoodmoves()
        {
/*            _context.Goodmoves.RemoveRange(_context.Goodmoves);
            await _context.SaveChangesAsync();
            string[] namegood1 = { "ТМЦ1", "ТМЦ2", "ТМЦ2" };
            string[] namestockfrom1 = { "Склад 1", "Склад 3", "Склад 1" };
            string[] namestocktowhere1 = { "Склад 2", "Склад 4", "Склад 2" };
            int[] qty1 = { 100, 50, 4 };
            for (int i = 0; i < 3; i++)
            {
                Goodmove GoodMoveAdded = new()
                {
                    NameGood = namegood1[i],
                    NameStockFrom = namestockfrom1[i],
                    NameStockTowhere = namestocktowhere1[i],
                    Qty = qty1[i]
                };
                _context.Goodmoves.Add(GoodMoveAdded);
            }
            await _context.SaveChangesAsync();*/
            return await _context.Goodmoves.ToListAsync();
        }

        // GET: api/Goodmoves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goodmove>> GetGoodmove(int id)
        {
            var goodmove = await _context.Goodmoves.FindAsync(id);

            if (goodmove == null)
            {
                return NotFound();
            }

            return goodmove;
        }

        // PUT: api/Goodmoves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutGoodmove(int id, Goodmove goodmove)
        {
/*           if (id != goodmove.Id)
            {
                return BadRequest();
            }*/

            _context.Entry(goodmove).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodmoveExists(goodmove.Id))
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

        // POST: api/Goodmoves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Goodmove>> PostGoodmove(Goodmove goodmove)
        {
            _context.Goodmoves.Add(goodmove);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodmove", new { id = goodmove.Id }, goodmove);
        }

        // DELETE: api/Goodmoves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodmove(int id)
        {
            var goodmove = await _context.Goodmoves.FindAsync(id);
            if (goodmove == null)
            {
                return NotFound();
            }

            _context.Goodmoves.Remove(goodmove);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoodmoveExists(int id)
        {
            return _context.Goodmoves.Any(e => e.Id == id);
        }
    }
}
