using CatalogoLivros.Context;
using CatalogoLivros.Helpers;
using CatalogoLivros.Models;
using CatalogoLivros.Service;
using Microsoft.EntityFrameworkCore;

namespace CatalogoLivros.Repositories
{
    public class BooksRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        private IBookRepository _bookRepository;

        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }
        // Update book
        public async Task<Book> Update(Book book)
        {
            _context.Entry<Book>(book).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
            return book;
        }


        // Get book by id
        public async Task<Book?> GetById(int id)
        {
            return await _context.Books.FindAsync(id);
            /*return await _context.Books
                .Include(t => t.Isbn)
                .Include(t => t.Title)
                .Include(t => t.Author)
                .Include(t => t.Price)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();*/
        }

        //Create Book
        public async Task<Book?> Create(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        // Get all Books
        public async Task<PaginatedList<Book>> GetBooks(string sort, string search, int currentPage, int pageSize)
        {
            PaginatedList<Book> response = new PaginatedList<Book>();

            var query = _context.Books.AsQueryable();

            if (sort.Count() > 0 && sort != null)
            {
                switch (sort.ToLower())
                {
                    case "isbn":
                        query = query.OrderBy(x => x.Isbn);
                        break;
                    case "title":
                        query = query.OrderBy(x => x.Title);
                        break;
                    case "author":
                        query = query.OrderBy(x => x.Author);
                        break;
                    case "price":
                        query = query.OrderBy(x => x.Price);
                        break;
                    default:
                        query = query.OrderBy(x => x.Id);
                        break;
                }

            }
            if (search.Count() > 0)
            {
                search = search.ToLower().Trim();
                query = query.Where(n => n.Title.Contains(search) || n.Isbn.ToString().Contains(search) || n.Author.Contains(search) || n.Price.ToString().Contains(search));

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
    }
}
