using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Store.Book.WEb.Services
{
    using Model;
    using Models;
    using Store.Book.Model.DTO;
    using Store.Book.WEb.Extensions;
    using System;
    using System.Linq;

    public class AuthorService
    {
        private ApplicationDbContext _db;

        public AuthorService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthors()
        {
            try
            {
                return await _db.Authors
                    .Select(p => p.Transform())
                    .ToListAsync();
            }
            catch (Exception exception)
            {
                // log (exception)
                return new List<AuthorDto>();
            }
        }
    }
}