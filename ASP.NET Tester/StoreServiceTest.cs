using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.Book.WEb.Services;
using Store.Book.WEb.Interfaces;
using Store.Book.WEb.Models;
using Store.Book.WEb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET.Tester
{
    [TestClass]
    public class StoreServiceTest
    {
        private readonly StoreService _store;
        private readonly Mock<ApplicationDbContext> mock;

        public StoreServiceTest()
        {
            _store = new StoreService(new ApplicationDbContext());
        }
        [TestMethod]
        public void GetBooksTest()
        {
            var mock = new Mock<IStoreService>();
            mock.Setup(a => a.GetBooks()).Returns(new List<Store.Book.Model.Book>
            {
                new Store.Book.Model.Book {Title="test0", Year=1820},
                new Store.Book.Model.Book {Title="test1", Year=1821},
                new Store.Book.Model.Book {Title="test2", Year=1822}
            });
            var books = mock.Object.GetBooks();
            var expected = 3;
            var fact = books.Count();
            Assert.AreEqual(expected, fact);

        }
    }
}
