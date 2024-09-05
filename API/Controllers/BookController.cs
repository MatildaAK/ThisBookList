using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();

            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound("Book not found.");

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book updateBook)
        {
            var dbBook = await _context.Books.FindAsync(updateBook.Id);
            if (dbBook is null)
                return NotFound("Book not found.");

            dbBook.Title = updateBook.Title;
            dbBook.Author = updateBook.Author;
            dbBook.Genre = updateBook.Genre;
            dbBook.ReleseDate = updateBook.ReleseDate;
            dbBook.Pages = updateBook.Pages;

            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var dbBook = await _context.Books.FindAsync(id);
            if (dbBook is null)
            { return NotFound("Book not found."); }

            _context.Books.Remove(dbBook);
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }

    }
}