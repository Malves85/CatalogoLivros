using CatalogoLivros.Context;
using CatalogoLivros.Entity;
using CatalogoLivros.Helpers;
using CatalogoLivros.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatalogoLivros.Repositories.Authors
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly AppDbContext _context;
        private IAuthorsRepository _authorsRepository;

        public AuthorsRepository(AppDbContext context)
        {
            _context = context;
        }
        // Update author
        public async Task<Author> Update(Author author)
        {
            _context.Entry<Author>(author).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
            return author;
        }


        // get all authors
        public async Task<PaginatedList<Author>> GetAuthors(string sort, string search, int currentPage, int pageSize)
        {
            PaginatedList<Author> response = new PaginatedList<Author>();

            var query = _context.Authors.Include(t => t.Books).AsQueryable();

            if (sort.Count() > 0 && sort != null)
            {
                switch (sort.ToLower())
                {
                    case "nome":
                        query = query.OrderBy(x => x.Name);
                        break;
                    case "país":
                        query = query.OrderBy(x => x.Nacionality);
                        break;
                    default:
                        query = query.OrderBy(x => x.Id);
                        break;
                }

            }
            if (search.Count() > 0)
            {
                search = search.ToLower().Trim();
                query = query.Where(n => n.Name.Contains(search) || n.Nacionality.Contains(search));

            }

            response.TotalRecords = query.Count();

            var numberOfItemsToSkip = pageSize * (currentPage - 1);

            query = query.Skip(numberOfItemsToSkip);
            query = query.Take(pageSize);

            var list = await query.ToListAsync();

            response.Items = list;
            response.CurrentPage = currentPage;
            response.PageSize = pageSize;
            response.Success = true;
            response.Message = null;

            return response;
        }

        // get author by id
        public async Task<Author?> GetById(int id)
        {
            return await _context.Authors.FindAsync(id);
        }


        // Create Author
        public async Task<Author?> Create(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }
    }
}
