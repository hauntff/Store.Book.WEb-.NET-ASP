using Store.Book.Model;
using System.Threading.Tasks;

namespace Store.Book.WEb.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author> 
    {
        Task<Author> GetByTitle(string title);
    }
}
