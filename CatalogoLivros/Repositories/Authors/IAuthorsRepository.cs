using CatalogoLivros.Entity;
using CatalogoLivros.Helpers;

namespace CatalogoLivros.Repositories.Authors
{
    public interface IAuthorsRepository
    {
        Task<PaginatedList<Author>> GetAuthors(string sorting, string searching, int currentPage, int pageSize);
        Task<Author?> Create(Author author);
        Task<Author?> GetById(int id);
        Task<Author> Update(Author author);
    }
}
