using Store.Book.Model;
using Store.Book.Model.DAO;
using Store.Book.WEb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Book.WEb.Controllers
{
    public class HomeController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IStoreService _store;
        public HomeController(IStoreService store)
        {
            _store = store;
        }

        public ActionResult Index()
        {
            logger.Info("its index. OK");
            return View(_store.GetBooks());
        }
        public ActionResult Buy(int id)
        {
            var book = _store.GetBookById(id);
            if (book is null)
                return Redirect("/");
            Order order = null;
            try
            {
                order = _store.CreateOrUpdateOrder(id, User.Identity.Name);
                ViewBag.Message = "Заказ успешно создан";
            }
            
            catch(Exception exception)
            {
                ViewBag.Error = exception.Message;
            }
            return View(order);
        }

        [Authorize(Roles = "ADMIN, MANAGER")]

        public ActionResult Test()
        {
            string userName = User.Identity.Name;
            var isAdmin = User.IsInRole("ADMIN");
            var isAuthenticated = User.Identity.IsAuthenticated;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dateStamp = HttpContext.Timestamp;
            var book = new Author { Title = "ТЕСТОВАЯ КНИГА" };
            return View(book);
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult BookSearch(string title)
        {
            var books = _store.FindBooks(title);
            return Json(books, JsonRequestBehavior.AllowGet);
        }
    }
}