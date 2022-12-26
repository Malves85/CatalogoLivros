using CatalogoLivros.Models;

namespace CatalogoLivros.Service
{
    public interface IBookService
    {
        //Task<IEnumerable<Book>> GetBooks();
        IEnumerable<Book> GetBooks(BooksParameters booksParameters);
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooksByIsbn(string isbn);
        Task<IEnumerable<Book>> searchBook(BooksParameters booksParameters, string item);
        Task<IEnumerable<Book>> sortBook(BooksParameters booksParameters, string sort);
        Task CreateBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBookById(Book book);
    }
}

