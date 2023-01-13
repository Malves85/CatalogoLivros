using CatalogoLivros.Entity;
using CatalogoLivros.Helpers;

namespace CatalogoLivros.Interface.Repositories
{
    public interface IAuthorsRepository
    {
        Task<PaginatedList<Author>> GetAuthors(string sorting, string searching, int currentPage, int pageSize);
        Task<Author?> Create(Author author);
        Task<Author?> GetById(int id);
        Task<Author> Update(Author author);
    }
}
