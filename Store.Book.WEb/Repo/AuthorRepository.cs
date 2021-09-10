using Store.Book.Model;
using Store.Book.WEb.Interfaces;
using Store.Book.WEb.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Book.WEb.Repos
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Author> GetByTitle(string title)
        {
            var result = await base.context.Authors
                .Where(p => p.Title == title).ToListAsync();

            var result0 = from p in base.context.Authors
            where p.Title == title
            select p;

            return result0.FirstOrDefault();
        }
    }
}
