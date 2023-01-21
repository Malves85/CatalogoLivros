using CatalogoLivros.Helpers;
using CatalogoLivros.Infrastructure.Models.Books;
using CatalogoLivros.Models.Books;

namespace CatalogoLivros.Interface.Services
{
    public interface IBookService
    {
        Task<MessagingHelper> DeleteBook(DeleteBook deleteBook);
        Task<MessagingHelper<BookDTO>> GetById(int id);
        Task<MessagingHelper<BookDTO>> Update(EditBook editBook);
        Task<MessagingHelper<int>> Create(CreateBook createBook);
        Task<PaginatedList<ListBook>> GetAll(Search search);

    }
}

