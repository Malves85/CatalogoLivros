using CatalogoLivros.Helpers;
using CatalogoLivros.Infrastructure.Models.Authors;
using CatalogoLivros.Models.Authors;

namespace CatalogoLivros.Interface.Services
{
    public interface IAuthorsService
    {
        Task<PaginatedList<ListAuthor>> GetAuthors(Search search);
        Task<MessagingHelper<int>> Create(CreateAuthor createAuthor);
        Task<MessagingHelper<AuthorDTO>> Update(EditAuthor editAuthor);
        Task<MessagingHelper> DeleteAuthor(DeleteAuthor deleteAuthor);
    }
}
