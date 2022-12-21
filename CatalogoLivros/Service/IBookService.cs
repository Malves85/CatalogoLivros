using CatalogoLivros.Models;

namespace CatalogoLivros.Service
{
    public interface IBookService
    {
        //Task<IEnumerable<Book>> GetBooks();
        IEnumerable<Book> GetBooks(BooksParameters booksParameters);
        Task<Book> GetBookById(int id);
        Task<Book> InsertBook(long isbn);
        Task<IEnumerable<Book>> searchBook(string item);
        Task CreateBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBookById(Book book);

    }
}

