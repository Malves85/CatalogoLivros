using CatalogoLivros.Helpers;
using CatalogoLivros.Models;
using CatalogoLivros.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace CatalogoLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("getAll")]
        public async Task<PaginatedList<ListBook>> GetAll(Search search)
        {
            return await _bookService.GetAll(search);
        }

        [HttpPost("create")]
        public async Task<MessagingHelper<int>> Create(CreateBook createBook)
        {
            return await _bookService.Create(createBook);
        }

        [HttpPost("update")]
        public async Task<MessagingHelper<BookDTO>> Update(EditBook editBook)
        {
            return await _bookService.Update(editBook);
        }

        [HttpGet("{id}")]
        public async Task<MessagingHelper<BookDTO>> GetById(int id)          
        {
            
            return await _bookService.GetById(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<MessagingHelper> Delete(int id)
        {
            return await _bookService.DeleteBook(id);
            
        }



    }
}
