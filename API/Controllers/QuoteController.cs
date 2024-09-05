using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly DataContext _context;

        public QuoteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Quote>>> GetAllQuotes()
        {
            var quotes = await _context.Quotes.ToListAsync();

            return Ok(quotes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote is null)
                return NotFound("Quote not found.");

            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<List<Quote>>> AddQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return Ok(await _context.Quotes.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Quote>>> UpdateQuote(Quote updateQuote)
        {
            var dbQuote = await _context.Quotes.FindAsync(updateQuote.Id);
            if (dbQuote is null)
                return NotFound("Book not found.");

            dbQuote.QuoteMessage = updateQuote.QuoteMessage;
            dbQuote.Author = updateQuote.Author;

            await _context.SaveChangesAsync();

            return Ok(await _context.Quotes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Quote>>> DeleteQuote(int id)
        {
            var dbQuote = await _context.Quotes.FindAsync(id);
            if (dbQuote is null)
            { return NotFound("Book not found."); }

            _context.Quotes.Remove(dbQuote);
            await _context.SaveChangesAsync();

            return Ok(await _context.Quotes.ToListAsync());
        }

    }
}