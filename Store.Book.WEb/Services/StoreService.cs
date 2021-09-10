using System;
using System.Linq;
using System.Collections.Generic;

namespace Store.Book.WEb.Services
{
    using Interfaces;
    using Model.DAO;
    using Models;

    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        private Price GetLastPrice(int bookId)
        {
            logger.Info($"GetLastPrice get price by bookId: {bookId}");
            var lastPrice = _context.Prices.Include("Book").Where(p => p.Book.Id == bookId)
                .OrderByDescending(p => p.Created).FirstOrDefault();
            if (!(lastPrice is null))
                logger.Info($"GetLastPrice price value: {lastPrice.Amount} by bookId: {bookId}");
            else logger.Warn($"GetLastPrice price value not found by bookId: {bookId}");
            return lastPrice;
        }
        private (Price, Exception) GetLastPriceEx(int bookId)
        {
            try
            {
                return (GetLastPrice(bookId), null);
            }
            catch (Exception exception)
            {
                logger.Error($"GetLastPriceEx error: {exception.Message}");
                return (null, exception);
            }
        }
        public Model.Book GetBookById(int bookId)
        {
            try
            {
                var book = _context.Books.Find(bookId);
                return book;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public Order CreateOrUpdateOrder(int bookId, string userName)
        {
            var book = GetBookById(bookId);
            if (book is null)
            {
                logger.Warn($"CreateOrder: book not found for bookId: {bookId}");
                throw new NullReferenceException($"book not found for bookId: {bookId}");
            }
            var (lastPrice, _) = GetLastPriceEx(bookId);
            if (lastPrice is null)
            {
                logger.Warn($"CreateOrder: lastPrice not found for bookId: {bookId}");
                throw new NullReferenceException($"lastPrice not found for bookId: {bookId}");
            }
            if (lastPrice.Amount < 0)
            {
                logger.Warn($"CreateOrder: price amount too low for bookId: {bookId}");
                throw new ArgumentOutOfRangeException($"lastPrice not found for bookId: {bookId}");
            }
            var order = _context
                .Orders
                .Where(p => p.UserName == userName && p.Status == Model.Enums.OrderStatusEnum.Created)
                .OrderByDescending(p => p.Created)
                .FirstOrDefault();
            if (order is null)
            {
                logger.Warn($"CreateOrder: last order for user: {userName} not found, creating new.");
                order = _context.Orders.Add(new Order
                {
                    UserName = userName,
                    Status = Model.Enums.OrderStatusEnum.Created,
                    Created = DateTime.Now,
                    Total = lastPrice.Amount
                });
            }
            AddToBusket(order, book, lastPrice);
            return order;
        }
        public bool AddToBusket(Order order, Model.Book book, Price price)
        {
            var existing = _context.Buskets
                .Include("Order")
                .Include("Book")
                .FirstOrDefault(p => p.Order.Id == order.Id && p.Book.Id == book.Id);
            if (!(existing is null))
            {
                logger.Warn($"AddToBusket: bookId: {book.Id} exist in busket");
                return false;
            }
            _context.Buskets.Add(new Busket { Order = order, Book = book, Price = price });
            return SafeSave();
        }
        public bool DeleteFromBusket(Order order, int bookId)
        {
            //var busket = ????;
            return false;
        }
        public Order FindOrder(int orderId)
        {
            return _context.Orders.FirstOrDefault(p => p.Id == orderId);
        }
        public bool CompleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }
        public bool SafeSave()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        public IEnumerable<Model.Book> GetBooks()
        {
            return _context.Books;
        }
        public IEnumerable<Model.Book> FindBooks(string title)
        {
            return _context.Books.Where(p => p.Title.ToLower().Contains(title.ToLower()));
        }
        public Order GetLastUnpayedOrder(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}