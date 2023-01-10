using CatalogoLivros.Entity;
using CatalogoLivros.Helpers;

namespace CatalogoLivros.Repositories.Books
{
    public interface IBookRepository
    {
        Task<bool> Exist(long id);
        Task<Book> Update(Book book);
        Task<Book?> GetById(int id);
        Task<Book?> Create(Book book);
        Task<PaginatedList<Book>> GetBooks(string searching, string sorting, int currentPage, int pageSize);
    }
}
