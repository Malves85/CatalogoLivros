using CatalogoLivros.Helpers;
using CatalogoLivros.Models;

namespace CatalogoLivros.Service
{
    public interface IBookService
    {
        Task<MessagingHelper<BookDTO>> GetById(int id);
        Task<MessagingHelper<BookDTO>> Update(EditBook editBook);
        Task<MessagingHelper<int>> Create(CreateBook createBook);
        Task<PaginatedList<ListBook>> GetAll(Search search);
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

