using CatalogoLivros.Helpers;
using CatalogoLivros.Models;

namespace CatalogoLivros.Repositories
{
    public interface IBookRepository
    {
        Task<Book> Update(Book book);
        Task<Book?> GetById(int id);
        Task<Book?> Create(Book book);
        Task<PaginatedList<Book>> GetBooks(string searching, string sorting, int currentPage, int pageSize);
    }
}
