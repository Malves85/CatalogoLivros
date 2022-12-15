using CatalogoLivros.Models;

namespace CatalogoLivros.Service
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> InsertBook(long isbn);
        Task<IEnumerable<Book>> GetBookByTitle(string title);
        Task CreateBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBookById(Book book);

    }
}

