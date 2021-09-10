using Store.Book.Model;
using Store.Book.Model.DAO;
using Store.Book.WEb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Book.WEb.Controllers
{
    public class SetupController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SetupController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Setup
        public ActionResult Index()
        {
            ViewBag.IsEmptyAuthors = !_context.Authors.Any();
            ViewBag.IsEmptyGenres = !_context.Genres.Any();
            ViewBag.IsEmptyBooks = !_context.Books.Any();
            ViewBag.IsEmptyPrices = !_context.Prices.Any();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string create_authors, string create_genres, string create_books, string create_prices)
        {
            if (!string.IsNullOrWhiteSpace(create_authors))
            {
                _context.Authors.Add(new Author { Title = "Тестовый автор 1" });
                _context.Authors.Add(new Author { Title = "Тестовый автор 2" });
                _context.Authors.Add(new Author { Title = "Тестовый автор 3" });
            }
            if (!string.IsNullOrWhiteSpace(create_genres))
            {
                _context.Genres.Add(new Genre { Title = "Тестовый жанр 1" });
                _context.Genres.Add(new Genre { Title = "Тестовый жанр 2" });
                _context.Genres.Add(new Genre { Title = "Тестовый жанр 3" });
            }
            if (!string.IsNullOrWhiteSpace(create_books))
            {
                _context.Books.Add(new Model.Book { Title = "Title 1" });
                _context.Books.Add(new Model.Book { Title = "Title 2" });
                _context.Books.Add(new Model.Book { Title = "Title 3" });
            }
            var ok = SafeSave();
            if (ok != "ok")
                ViewBag.Error = ok;
            else
            {
                if (!string.IsNullOrWhiteSpace(create_prices))
                {
                    var book1 = _context.Books.FirstOrDefault(p => p.Title == "Title 1");
                    var book2 = _context.Books.FirstOrDefault(p => p.Title == "Title 2");
                    var book3 = _context.Books.FirstOrDefault(p => p.Title == "Title 2");

                    _context.Prices.Add(new Price { Book = book1, Amount = 100, Created = DateTime.Now });
                    _context.Prices.Add(new Price { Book = book2, Amount = 100, Created = DateTime.Now.AddMonths(-1) });
                    _context.Prices.Add(new Price { Book = book3, Amount = 150, Created = DateTime.Now });
                }
                SafeSave(); 
            }
            ViewBag.IsEmptyAuthors = !_context.Authors.Any();
            ViewBag.IsEmptyGenres = !_context.Genres.Any();
            ViewBag.IsEmptyBooks = !_context.Books.Any();
            ViewBag.IsEmptyPrices = !_context.Prices.Any();
            return View();
        }
        private string SafeSave()
        {
            try
            {
                _context.SaveChanges();
                return "ok";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

        }
    }
}