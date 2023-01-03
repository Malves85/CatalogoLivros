using CatalogoLivros.Context;
using CatalogoLivros.Helpers;
using CatalogoLivros.Models;
using CatalogoLivros.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;

namespace CatalogoLivros.Service
{
    public class BooksService : IBookService
    {
        private readonly AppDbContext _context;
        private IBookService _bookService;
        private IBookRepository _bookRepository;

        public BooksService(IBookRepository bookRepository, AppDbContext context)
        {
            _context = context;
            _bookRepository = bookRepository;
        }
        // Get book by id

        public async Task<MessagingHelper<BookDTO>> GetById(int id)
        {
            MessagingHelper<BookDTO> result = new();                  
            try
            {
                var responseRepository = await _bookRepository.GetById(id);
                if (responseRepository == null)
                {
                    result.Success = false;
                    result.Message = "Não foi possivel encontrar este livro";
                    return result;
                }
                var assignmentResponse = new BookDTO(responseRepository);

                result.Obj = assignmentResponse;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocorreu um erro ao ir buscar livro";
            }
            return result;
        }


        // Update book
        public async Task<MessagingHelper<BookDTO>> Update(EditBook editBook)
        {
            MessagingHelper<BookDTO> result = new();
            try
            {
                EditBookValidator validator = new();
                var responseValidator = validator.Validate(editBook);
                if (responseValidator.IsValid == false)                
                {
                    result.Success = false;
                    result.Message = responseValidator.Errors.FirstOrDefault().ErrorMessage;
                    return result;
                }
                var bookDB = await _bookRepository.GetById(editBook.Id);
                if (bookDB == null)
                {
                    result.Message = "Este livro não existe";
                    return result;
                }    

                bookDB.Isbn = editBook.Isbn;
                bookDB.Title = editBook.Title;
                bookDB.Author = editBook.Author;                 
                bookDB.Price = editBook.Price;

                var bookUpDate = await _bookRepository.Update(bookDB);

                result.Success = true;
                result.Obj = new BookDTO(bookUpDate);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Erro ao editar o livro";
            }

            return result;
        }

        // Create book
        public async Task<MessagingHelper<int>> Create(CreateBook createBook)
        {
            MessagingHelper<int> response = new();

            try
            {
                var responseValidate = await new CreateBookValidator().ValidateAsync(createBook);
                if (responseValidate == null || responseValidate.IsValid == false)
                {
                    response.Message = responseValidate == null ? "Erro ao validar a informação para criar um livro" : responseValidate.Errors.FirstOrDefault()!.ErrorMessage;
                    response.Success = false;
                    return response;
                }
                var newBook = createBook.ToEntity();
                var assistanceInDB = await _bookRepository.Create(newBook); 
                if (assistanceInDB == null)
                {
                    response.Message = "Erro ao criar o livro!";
                    return response;
                }
                response.Success = true;
                response.Obj = assistanceInDB.Id;
                response.Message = "Livro criado com sucesso";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro inesperado ao criar o livro";
            }

            return response;
        }

        // Get all books
        public async Task<PaginatedList<ListBook>> GetAll(Search search)
        {
            PaginatedList<ListBook> response = new();

            try
            {
                if (search.PageSize > 100)
                {
                    search.PageSize = 100;
                }

                if (search.PageSize <= 0)
                {
                    search.PageSize = 1;
                }

                if (search.CurrentPage <= 0)
                {
                    search.CurrentPage = 1;
                }

                //obter a informação
                var responseRepository = await _bookRepository.GetBooks(search.Sorting, search.Searching, search.CurrentPage, search.PageSize);
                if (responseRepository.Success != true)
                {
                    response.Success = false;
                    response.Message = "Erro ao obter a informação das urdissagens";
                    return response;
                }

                response.Items = responseRepository.Items.Select(t => new ListBook(t)).ToList();
                response.PageSize = responseRepository.PageSize;
                response.CurrentPage = responseRepository.CurrentPage;
                response.TotalRecords = responseRepository.TotalRecords;
                response.Success = true;
                            }
                            catch (Exception ex)
                {
                    
                    response.Success = false;
                    response.Message = "Ocorreu um erro ao obter os livros.";
                }

                return response;
        }

        /*public async Task<PaginatedList<Book>> GetAll(Search search )
        {
            PaginatedList<Book> response = new PaginatedList<Book>();

            var query = _context.Books.AsQueryable();

            if (order.Count() > 0 && order != null)
            {
                switch (order.ToLower())
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

        }*/
        /*public IEnumerable<Book> GetBooks(BooksParameters booksParameters)
        {
            return await _context.Books
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
                return _context.Books
                .OrderBy(on => on.Id)
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToList();
            }
            else if (sort == "Isbn")
            {
                return _context.Books
            .OrderBy(on => on.Isbn)
            .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
            .Take(booksParameters.PageSize)
            .ToList();
            }
            else if (sort == "Author")
            {
                return _context.Books
             .OrderBy(on => on.Author)
             .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
             .Take(booksParameters.PageSize)
             .ToList();
            }
            else if (sort == "Price")
            {
               return _context.Books
            .OrderBy(on => on.Price)
            .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
            .Take(booksParameters.PageSize)
            .ToList();
            }
            else
            {
                return _context.Books
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
