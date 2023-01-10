using CatalogoLivros.Helpers;
using CatalogoLivros.Models.Authors;

namespace CatalogoLivros.Services.Authors
{
    public interface IAuthorsService
    {
        Task<PaginatedList<ListAuthor>> GetAuthors(Search search);
        Task<MessagingHelper<int>> Create(CreateAuthor createAuthor);
        Task<MessagingHelper<AuthorDTO>> Update(EditAuthor editAuthor);
        Task<MessagingHelper> DeleteAuthor(int id);
    }
}
