using Store.Book.Model.DAO;
using Store.Book.WEb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Book.WEb.Interfaces
{
    public interface IStoreService
    {
        Model.Book GetBookById(int bookId);
        Order CreateOrUpdateOrder(int bookId, string userName);
        bool AddToBusket(Order order, Model.Book book, Price price);
        bool DeleteFromBusket(Order order, int bookId);
        bool CompleteOrder(int orderId);
        Order FindOrder(int orderId);
        IEnumerable<Model.Book> FindBooks(string title);
        //Order GetLastUnpayedOrder(string userName);
        IEnumerable<Model.Book> GetBooks();
    }
}