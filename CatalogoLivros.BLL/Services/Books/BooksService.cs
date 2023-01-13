using CatalogoLivros.Context;
using CatalogoLivros.Helpers;
using CatalogoLivros.Infrastructure.Models.Books;
using CatalogoLivros.Interface.Repositories;
using CatalogoLivros.Interface.Services;
using CatalogoLivros.Models.Books;
using System.Data;

namespace CatalogoLivros.Services.Books
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

        // Delete book
        public async Task<MessagingHelper> DeleteBook(DeleteBook deleteBook)
        {
            MessagingHelper result = new();
            try
            {
                var responseRepository = await _bookRepository.GetById(deleteBook.Id);
                if (responseRepository == null)
                {
                    result.Success = false;
                    result.Message = "Não foi possivel encontrar este livro";
                    return result;
                }

                //_context.Entry(responseRepository).CurrentValues["isDeleted"] = true;
                responseRepository.DeleteBook();
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Livro deletado com sucesso";
                return result;

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
                if (editBook.Isbn == bookDB.Isbn && editBook.Title == bookDB.Title && editBook.Price == bookDB.Price && editBook.AuthorId == bookDB.AuthorId)
                {
                    result.Success = false;
                    result.Message = "Nenhuma alteração feita";
                    return result;
                }

                bookDB.Isbn = editBook.Isbn;
                bookDB.Title = editBook.Title;
                bookDB.AuthorId = editBook.AuthorId;                 
                bookDB.Price = editBook.Price;

                var bookUpDate = await _bookRepository.Update(bookDB);

                result.Success = true;
                result.Message = "Livro editado com sucesso";
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
                var isbnExist = await _bookRepository.Exist(createBook.Isbn);
                if (isbnExist == true)
                {
                    response.Success = false;
                    response.Message = "Já existe um livro com esse Isbn";
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

                var responseRepository = await _bookRepository.GetBooks(search.Sorting, search.Searching, search.CurrentPage, search.PageSize);
                if (responseRepository.Success != true)
                {
                    response.Success = false;
                    response.Message = "Erro ao obter a informação do livro";
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


    }
}
