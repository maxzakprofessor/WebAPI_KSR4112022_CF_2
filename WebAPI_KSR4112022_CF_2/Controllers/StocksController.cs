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
    public class StocksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
/*            _context.Stocks.RemoveRange(_context.Stocks);
            await _context.SaveChangesAsync();

            string namestock1 = "Склад ";
            for (int i = 1; i < 10; i++)
            {
                Stock StockAdded = new()
                {
                    NameStock = namestock1 + i.ToString(),
                };


                _context.Stocks.Add(StockAdded);

            }
            await _context.SaveChangesAsync();*/
            return await _context.Stocks.ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{id}/{name}")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStock(int id, string name)
        {
            var stock = await _context.Stocks.FindAsync(id);
            var stock_l1 = from p in _context.Stocks where p.NameStock==name select p ;
            if (stock == null)
            {
                return NotFound();
            }

            return await stock_l1.ToListAsync();
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStock(Stock stock)
        {
 /*           if (id != stock.Id)
            {
                return BadRequest();
            }*/

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(stock.Id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return NoContent();
            //return CreatedAtAction("GetStock", new { id = stock.Id }, stock);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}
