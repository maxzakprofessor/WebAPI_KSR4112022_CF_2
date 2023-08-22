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
    public class GoodincomesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GoodincomesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Goodincomes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goodincome>>> GetGoodincomes()
        {
            
/*            _context.Goodincomes.RemoveRange(_context.Goodincomes);
            await _context.SaveChangesAsync();

            string[] namegood1 = { "ТМЦ1", "ТМЦ1", "ТМЦ7" };
            string[] namestock1 = { "Склад 1", "Склад 1", "Склад 4" };
            int[] qty1 = { 200, 500, 4500 };
            for (int i = 0; i < 3; i++)
            {
                Goodincome GoodIncomeAdded = new()
                {
                    NameGood = namegood1[i],
                    NameStock = namestock1[i],
                    Qty = qty1[i]
                };
                _context.Goodincomes.Add(GoodIncomeAdded);
            }
            await _context.SaveChangesAsync();*/
            return await _context.Goodincomes.ToListAsync();
        }

        // GET: api/Goodincomes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goodincome>> GetGoodincome(int id)
        {
            var goodincome = await _context.Goodincomes.FindAsync(id);

            if (goodincome == null)
            {
                return NotFound();
            }

            return goodincome;
        }

        // PUT: api/Goodincomes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]//Убрать id в скобках
        public async Task<IActionResult> PutGoodincome(int id, Goodincome goodincome)
        {
/*            if (id != goodincome.Id)
            {
                return BadRequest();
            }*/

            _context.Entry(goodincome).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodincomeExists(goodincome.Id))
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

        // POST: api/Goodincomes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Goodincome>> PostGoodincome(Goodincome goodincome)
        {
            _context.Goodincomes.Add(goodincome);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodincome", new { id = goodincome.Id }, goodincome);
        }

        // DELETE: api/Goodincomes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodincome(int id)
        {
            var goodincome = await _context.Goodincomes.FindAsync(id);
            if (goodincome == null)
            {
                return NotFound();
            }

            _context.Goodincomes.Remove(goodincome);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoodincomeExists(int id)
        {
            return _context.Goodincomes.Any(e => e.Id == id);
        }
    }
}
