using CatalogoLivros.Context;
using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoLivros.Service
{
    public class BooksService : IBookService
    {
        private readonly AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks(BooksParameters booksParameters)
        {
            return FindAll()
                .OrderBy(on => on.Title)
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToList();
        }
        public IQueryable<Book> FindAll()
        {
            return this._context.Set<Book>();
        }

        /*public async Task<IEnumerable<Book>> GetBooks()
        {
            try
            {   // retorna os livros ordenados por título
                return await _context.Books.OrderBy(t => t.Title).ToListAsync();

                // retorna os livros ordenados por id
                //return await _context.Books.ToListAsync();
            }
            catch
            {
                throw;
            }

        }*/
        public async Task<IEnumerable<Book>> GetBookByTitle(string title)
        {
            IEnumerable<Book> books;
            if (!string.IsNullOrWhiteSpace(title))
            {
                books = await _context.Books.Where(n => n.Title.Contains(title)).ToListAsync();
            }
            else
            {
                books = await _context.Books.ToListAsync();
                //books = await _context.Books.Where(n => n.Title.Contains(title)).ToListAsync();
                //books = await GetBooks();
            }
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book;

        }
        public async Task<IEnumerable<Book>> InsertBook(long isbn)
        {
            IEnumerable<Book> books;
            books = (IEnumerable<Book>)await _context.Books.FindAsync(isbn);
            return books;

        }

        public async Task CreateBook(Book book)
        {
             
            _context.Books.Add(book);
                await _context.SaveChangesAsync();
                       
        }

        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteBookById(Book book)
        {
            _context.Entry(book).CurrentValues["isDeleted"] = true;
            await _context.SaveChangesAsync();
        }
    }
}
