using CatalogoLivros.Helpers;
using CatalogoLivros.Models;

namespace CatalogoLivros.Service
{
    public interface IBookService
    {
        Task<PaginatedList<Book>> GetBooks(int currentPage = 1, int pageSize = 5);
        //Task<IEnumerable<Book>> GetBooks();
        //IEnumerable<Book> GetBooks(BooksParameters booksParameters);
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooksByIsbn(string isbn);
        Task<IEnumerable<Book>> searchBook(BooksParameters booksParameters, string item);
        Task<IEnumerable<Book>> sortBook(BooksParameters booksParameters, string sort);
        Task<MessagingHelper<int>> CreateBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBookById(Book book);
    }
}

