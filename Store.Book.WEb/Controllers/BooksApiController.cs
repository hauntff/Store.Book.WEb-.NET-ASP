using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Store.Book.Model;
using Store.Book.WEb.Models;

namespace Store.Book.WEb.Controllers
{
    public class BooksApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BooksApi
        public IQueryable<Model.Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/BooksApi/5
        [ResponseType(typeof(Model.Book))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/BooksApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Model.Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BooksApi
        [ResponseType(typeof(Model.Book))]
        public async Task<IHttpActionResult> PostBook(Model.Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/BooksApi/5
        [ResponseType(typeof(Model.Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}