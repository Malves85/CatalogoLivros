using Azure.Core;
using CatalogoLivros.Context;
using CatalogoLivros.Helpers;
using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;

namespace CatalogoLivros.Service
{
    public class BooksService : IBookService
    {
        private readonly AppDbContext _context;
        private IBookService _bookService;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Book>> GetBooks(int currentPage = 1, int pageSize = 5)
        {
            PaginatedList<Book> response = new PaginatedList<Book>();

            var query = _context.Books.AsQueryable();
            

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
        /*public IEnumerable<Book> GetBooks(BooksParameters booksParameters)
        {
            return FindAll()
                .OrderBy(on => on.Title)
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToList();
        }*/
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

        public async Task<IEnumerable<Book>> searchBook(BooksParameters booksParameters, string item)
        {
            IEnumerable<Book> books;
            item = item.ToLower();

            return books = await _context.Books
                .Where(n => n.Title.Contains(item) || n.Isbn.ToString().Contains(item) || n.Author.Contains(item) || n.Price.ToString().Contains(item))
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> sortBook(BooksParameters booksParameters, string sort)
        {
            IEnumerable<Book> books;

            if (sort == "Id")
            {
                return FindAll()
                .OrderBy(on => on.Id)
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToList();
            }
            else if (sort == "Isbn")
            {
                return FindAll()
            .OrderBy(on => on.Isbn)
            .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
            .Take(booksParameters.PageSize)
            .ToList();
            }
            else if (sort == "Author")
            {
                return FindAll()
             .OrderBy(on => on.Author)
             .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
             .Take(booksParameters.PageSize)
             .ToList();
            }
            else if (sort == "Price")
            {
               return FindAll()
            .OrderBy(on => on.Price)
            .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
            .Take(booksParameters.PageSize)
            .ToList();
            }
            else
            {
                return FindAll()
            .OrderBy(on => on.Title)
            .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
            .Take(booksParameters.PageSize)
            .ToList();
            }
           

        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book;

        }

        public async Task<IEnumerable<Book>> GetBooksByIsbn(string isbn)
        {

            IEnumerable<Book> books;

            if (!string.IsNullOrEmpty(isbn))
            {
                books = await _context.Books.Where(b => (b.Isbn.ToString()).Contains(isbn)).ToListAsync();
            }
            else
            {
                books = await _context.Books.ToListAsync();
            }
            return books;
        }

        public async Task<MessagingHelper<int>> CreateBook(Book book)
        {
            MessagingHelper<int> response = new();

            try
            {
                //var hasIsbn = await _bookService.GetBooksByIsbn(book.Isbn.ToString());

                /*if (hasIsbn.Any() == true)
                {
                    response.Success = false;
                    response.Message = "Esse Isbn já existe";
                    return response;
                }*/
                 _context.Books.Add(book);
                 await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Livro criado com sucesso";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro inesperado ao criar um livro";
            }
            return response;
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
