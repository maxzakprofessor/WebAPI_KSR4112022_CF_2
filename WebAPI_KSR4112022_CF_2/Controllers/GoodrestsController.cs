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
    public class GoodrestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GoodrestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Goodrests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goodrest>>> GetGoodrests()
        {

            return await _context.Goodrests.ToListAsync();
        }
 
        // GET: api/Goodrests/5
        [HttpGet("{wnameStock}/{wnameGood}")]
        public async Task<ActionResult<IEnumerable<Goodrest>>> GetGoodrest(string wnameStock, string wnameGood)
        {
           // When many stock and good is one any!!!
            if (wnameStock == "Все" && wnameGood=="Все" )
            {

                List<Good> good_l = new List<Good>(_context.Goods);
                var g_name = good_l.Select(p => p.NameGood).ToArray();
                List<Stock> stock_l = new List<Stock>(_context.Stocks);
                var s_name = stock_l.Select(p => p.NameStock).ToArray();
                List<Goodincome> goodincomes_l = new List<Goodincome>(_context.Goodincomes);
                List<Goodmove> goodmoves_l = new List<Goodmove>(_context.Goodmoves);
                List<Goodrest> goodrests_l = new List<Goodrest>();

                //foreach (Good g in good_l)
                for (int ii = 0; ii < g_name.Count(); ii++)
                {
                    for (int jj = 0; jj < s_name.Count(); jj++)
                    //    foreach (Stock s in stock_l)
                    {
                        int Qty_sum = 0;
                        if (goodincomes_l.Any(p => p.NameGood == g_name[ii] && p.NameStock == s_name[jj]))
                        {
                            Qty_sum = goodincomes_l.Where(p => p.NameGood == g_name[ii]
                                            && p.NameStock == s_name[jj]).Sum(p => p.Qty);
                        };
                        if (goodmoves_l.Any(p => p.NameGood == g_name[ii]
                                && p.NameStockFrom == s_name[jj]))
                        {
                            Qty_sum = Qty_sum - goodmoves_l.Where(p => p.NameGood == g_name[ii]
                                && p.NameStockFrom == s_name[jj]).Sum(p => p.Qty);
                        };
                        if (goodmoves_l.Any(p => p.NameGood == g_name[ii]
                                && p.NameStockTowhere == s_name[jj]))
                        {
                            Qty_sum = Qty_sum + goodmoves_l.Where(p => p.NameGood == g_name[ii]
                                && p.NameStockTowhere == s_name[jj]).Sum(p => p.Qty);
                        }

                        if (Qty_sum > 0)
                        {
                            Goodrest goodrests_l1 = new Goodrest();
                            goodrests_l1.NameGood = g_name[ii];
                            goodrests_l1.NameStock = s_name[jj];
                            goodrests_l1.Qty = Qty_sum;

                            goodrests_l.Add(goodrests_l1);
                            //            Console.WriteLine($"Sum: {Qty_sum_Goodincomes}");
                        }
                    }
                }
                _context.Goodrests.RemoveRange(_context.Goodrests);
                await _context.SaveChangesAsync();
                await _context.Goodrests.AddRangeAsync(goodrests_l);
                await _context.SaveChangesAsync();

                return await _context.Goodrests.ToListAsync();
            }

            // When one any stock and good is many!!!
            if (wnameStock != "Все" && wnameGood == "Все") 
            {
                List<Good> good_l = new List<Good>(_context.Goods);
                var g_name = good_l.Select(p => p.NameGood).ToArray();
                List<Stock> stock_l = new List<Stock>(_context.Stocks);
                var s_name = stock_l.Select(p => p.NameStock).ToArray();
                List<Goodincome> goodincomes_l = new List<Goodincome>(_context.Goodincomes);
                List<Goodmove> goodmoves_l = new List<Goodmove>(_context.Goodmoves);
                List<Goodrest> goodrests_l = new List<Goodrest>();

                //foreach (Good g in good_l)
                for (int ii = 0; ii < g_name.Count(); ii++)
                {

                        int Qty_sum = 0;
                        if (goodincomes_l.Any(p => p.NameGood == g_name[ii] && p.NameStock == wnameStock))
                        {
                            Qty_sum = goodincomes_l.Where(p => p.NameGood == g_name[ii]
                                            && p.NameStock == wnameStock).Sum(p => p.Qty);
                        };
                        if (goodmoves_l.Any(p => p.NameGood == g_name[ii]
                                && p.NameStockFrom == wnameStock))
                        {
                            Qty_sum = Qty_sum - goodmoves_l.Where(p => p.NameGood == g_name[ii]
                                && p.NameStockFrom == wnameStock).Sum(p => p.Qty);
                        };
                        if (goodmoves_l.Any(p => p.NameGood == g_name[ii]
                                && p.NameStockTowhere == wnameStock))
                        {
                            Qty_sum = Qty_sum + goodmoves_l.Where(p => p.NameGood == g_name[ii]
                                && p.NameStockTowhere == wnameStock).Sum(p => p.Qty);
                        }

                        if (Qty_sum > 0)
                        {
                            Goodrest goodrests_l1 = new Goodrest();
                            goodrests_l1.NameGood = g_name[ii];
                            goodrests_l1.NameStock = wnameStock;
                            goodrests_l1.Qty = Qty_sum;

                            goodrests_l.Add(goodrests_l1);
                            //            Console.WriteLine($"Sum: {Qty_sum_Goodincomes}");
                        }
                    
                }
                _context.Goodrests.RemoveRange(_context.Goodrests);
                await _context.SaveChangesAsync();
                await _context.Goodrests.AddRangeAsync(goodrests_l);
                await _context.SaveChangesAsync();

                return await _context.Goodrests.ToListAsync();

            }

            // When many stock and good is one any!!!
            if (wnameStock == "Все" && wnameGood != "Все")
            {

                List<Good> good_l = new List<Good>(_context.Goods);
                var g_name = good_l.Select(p => p.NameGood).ToArray();
                List<Stock> stock_l = new List<Stock>(_context.Stocks);
                var s_name = stock_l.Select(p => p.NameStock).ToArray();
                List<Goodincome> goodincomes_l = new List<Goodincome>(_context.Goodincomes);
                List<Goodmove> goodmoves_l = new List<Goodmove>(_context.Goodmoves);
                List<Goodrest> goodrests_l = new List<Goodrest>();

                //foreach (Good g in good_l)
                    for (int jj = 0; jj < s_name.Count(); jj++)
                    //    foreach (Stock s in stock_l)
                    {
                        int Qty_sum = 0;
                        if (goodincomes_l.Any(p => p.NameGood == wnameGood && p.NameStock == s_name[jj]))
                        {
                            Qty_sum = goodincomes_l.Where(p => p.NameGood == wnameGood
                                            && p.NameStock == s_name[jj]).Sum(p => p.Qty);
                        };
                        if (goodmoves_l.Any(p => p.NameGood == wnameGood
                                && p.NameStockFrom == s_name[jj]))
                        {
                            Qty_sum = Qty_sum - goodmoves_l.Where(p => p.NameGood == wnameGood
                                && p.NameStockFrom == s_name[jj]).Sum(p => p.Qty);
                        };
                        if (goodmoves_l.Any(p => p.NameGood == wnameGood
                                && p.NameStockTowhere == s_name[jj]))
                        {
                            Qty_sum = Qty_sum + goodmoves_l.Where(p => p.NameGood == wnameGood
                                && p.NameStockTowhere == s_name[jj]).Sum(p => p.Qty);
                        }

                        if (Qty_sum > 0)
                        {
                            Goodrest goodrests_l1 = new Goodrest();
                            goodrests_l1.NameGood = wnameGood;
                            goodrests_l1.NameStock = s_name[jj];
                            goodrests_l1.Qty = Qty_sum;

                            goodrests_l.Add(goodrests_l1);
                            //            Console.WriteLine($"Sum: {Qty_sum_Goodincomes}");
                        }
                    
                }
                _context.Goodrests.RemoveRange(_context.Goodrests);
                await _context.SaveChangesAsync();
                await _context.Goodrests.AddRangeAsync(goodrests_l);
                await _context.SaveChangesAsync();

                return await _context.Goodrests.ToListAsync();
            }
            
            // When many stock and good is one any!!!
            if (wnameStock != "Все" && wnameGood != "Все")
            {

                List<Goodincome> goodincomes_l = new List<Goodincome>(_context.Goodincomes);
                List<Goodmove> goodmoves_l = new List<Goodmove>(_context.Goodmoves);
                List<Goodrest> goodrests_l = new List<Goodrest>();

                    int Qty_sum = 0;
                    if (goodincomes_l.Any(p => p.NameGood == wnameGood && p.NameStock == wnameStock))
                    {
                        Qty_sum = goodincomes_l.Where(p => p.NameGood == wnameGood
                                        && p.NameStock == wnameStock).Sum(p => p.Qty);
                    };
                    if (goodmoves_l.Any(p => p.NameGood == wnameGood
                            && p.NameStockFrom == wnameStock))
                    {
                        Qty_sum = Qty_sum - goodmoves_l.Where(p => p.NameGood == wnameGood
                            && p.NameStockFrom == wnameStock).Sum(p => p.Qty);
                    };
                    if (goodmoves_l.Any(p => p.NameGood == wnameGood
                            && p.NameStockTowhere == wnameStock))
                    {
                        Qty_sum = Qty_sum + goodmoves_l.Where(p => p.NameGood == wnameGood
                            && p.NameStockTowhere == wnameStock).Sum(p => p.Qty);
                    }

                    if (Qty_sum > 0)
                    {
                        Goodrest goodrests_l1 = new Goodrest();
                        goodrests_l1.NameGood = wnameGood;
                        goodrests_l1.NameStock = wnameStock;
                        goodrests_l1.Qty = Qty_sum;

                        goodrests_l.Add(goodrests_l1);
                        //            Console.WriteLine($"Sum: {Qty_sum_Goodincomes}");
                    }
     
                _context.Goodrests.RemoveRange(_context.Goodrests);
                await _context.SaveChangesAsync();
                await _context.Goodrests.AddRangeAsync(goodrests_l);
                await _context.SaveChangesAsync();

                return await _context.Goodrests.ToListAsync();
            }
            return NoContent();
        }

        // PUT: api/Goodrests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoodrest(int id, Goodrest goodrest)
        {
            if (id != goodrest.Id)
            {
                return BadRequest();
            }

            _context.Entry(goodrest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodrestExists(id))
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

        // POST: api/Goodrests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Goodrest>> PostGoodrest(Goodrest goodrest)
        {
            _context.Goodrests.Add(goodrest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodrest", new { id = goodrest.Id }, goodrest);
        }

        // DELETE: api/Goodrests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodrest(int id)
        {
            var goodrest = await _context.Goodrests.FindAsync(id);
            if (goodrest == null)
            {
                return NotFound();
            }

            _context.Goodrests.Remove(goodrest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoodrestExists(int id)
        {
            return _context.Goodrests.Any(e => e.Id == id);
        }
    }
}
