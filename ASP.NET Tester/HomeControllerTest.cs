using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Book.WEb.Services;
using Store.Book.WEb.Controllers;
using Store.Book.WEb.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASP.NET_Tester
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly StoreService _store;
        private HomeController controller;
        private ViewResult result;
        public HomeControllerTest()
        {
            _store = new StoreService(new ApplicationDbContext());
        }
        [TestInitialize]
        public void SetupContext()
        {
            //Arrange
            controller = new HomeController(_store);
            //Act
            result = controller.Index() as ViewResult;
        }
        [TestMethod]
        public void IndexViewresultNotNull()
        {
            //Arrange
            HomeController controller = new HomeController(_store);
            //Act
            ViewResult result = controller.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            
            var books = (List<Store.Book.Model.Book>) result.Model;
            var books1 = result.Model as List<Store.Book.Model.Book>;
            var count = books.Count;
            Assert.AreEqual(count,0);
        }
       
    }
}
