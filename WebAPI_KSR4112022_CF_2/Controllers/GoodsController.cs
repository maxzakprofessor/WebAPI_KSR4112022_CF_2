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
    public class GoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Goods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Good>>> GetGoods()
        {
 /*           _context.Goods.RemoveRange(_context.Goods);
            await _context.SaveChangesAsync();

            string namegood1 = "ТМЦ";
            for (int i = 1; i < 10; i++)
            {
                Good GoodAdded = new()
                {
                    NameGood = namegood1 + i.ToString(),
                };


                _context.Goods.Add(GoodAdded);

            }
            await _context.SaveChangesAsync();*/
            return await _context.Goods.ToListAsync();
        }

        // GET: api/Goods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Good>>> GetGood(int id)
        {
            var good = await _context.Goods.FindAsync(id);
            var good_l1 = from p in _context.Goods where p.Id == id select p;

            if (good == null)
            {
                return NotFound();
            }
            //return await _context.Goods.ToListAsync();
            return await good_l1.ToListAsync();
        }

        // PUT: api/Goods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutGood(Good good)
        {
 /*           if (id != good.Id)
            {
                return BadRequest();
            }*/
            
            _context.Entry(good).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodExists(good.Id))
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

        // POST: api/Goods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Good>> PostGood(Good good)
        {
            _context.Goods.Add(good);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGood", new { id = good.Id }, good);
        }

        // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGood(int id)
        {
            var good = await _context.Goods.FindAsync(id);
            if (good == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(good);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoodExists(int id)
        {
            return _context.Goods.Any(e => e.Id == id);
        }
    }
}
