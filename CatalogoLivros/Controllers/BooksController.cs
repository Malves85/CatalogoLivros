using CatalogoLivros.Models;
using CatalogoLivros.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetBooks([FromQuery] BooksParameters booksParameters)
        {
            try
            {
                var books = _bookService.GetBooks(booksParameters);
                return Ok(books);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter livros");


            }
        }

        /*public async Task<ActionResult<IAsyncEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetBooks();
                return Ok(books);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter livros");
            }
        }*/

        [HttpGet("searchBook")]

        public async Task<ActionResult<IAsyncEnumerable<Book>>>
            searchBook([FromQuery] BooksParameters booksParameters, string item)
        {
            try
            {
                if (item.Count() <= 1)
                {
                    return NotFound($"é preciso colocar no minimo 2 caracteres");
                    //return NotFound($"Não existem livros com o critério {item}");
                }
            
                var books = await _bookService.searchBook(booksParameters, item);
                return Ok(books);
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter livros");
            }
        }


        [HttpGet("sortBook")]

        public async Task<ActionResult<IAsyncEnumerable<Book>>>
            sortBook([FromQuery] BooksParameters booksParameters, string sort)
        {
            try
            {
                var books = await _bookService.sortBook(booksParameters, sort);
                return Ok(books);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter livros");
            }
        }


        [HttpGet("{id:int}", Name = "GetBookById")]
        public async Task<ActionResult<IAsyncEnumerable<Book>>> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookById(id);
                if (book == null)
                {
                    return NotFound($"Não existem livros com o id = {id}");
                }
                    


                return Ok(book);
            }
            catch
            {
                return BadRequest("Inválid Request");
            }
        }

        [HttpPost]

        public async Task<ActionResult> Create(Book book)
        {
            try
            {
                var hasIsbn = await _bookService.GetBooksByIsbn(book.Isbn.ToString());

                if (hasIsbn.Any() == true)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Esse ISBN {book.Isbn} já existe");
                }
                await _bookService.CreateBook(book);
                return Ok(book);
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        /*public async Task<ActionResult> Create(Book book)
        {
            try
            {
                if (book.Price > 0)
                {
                    await _bookService.CreateBook(book);
                    return CreatedAtRoute(nameof(GetBookById), new { id = book.Id }, book);

                    //var isbn = _bookService.InsertBook(book.Isbn).ToString();
                    //if (isbn.Any() != true)
                    //{
                        //await _bookService.CreateBook(book);
                        //return CreatedAtRoute(nameof(GetBookById), new { id = book.Id }, book);
                    //}
                    //else
                    //{
                         //return BadRequest("Isbn já existe");
                    //}
                    
                }
                else
                {
                    return BadRequest("price incorreto");
                }
                
            }
            catch
            {

                return BadRequest("Inválid Request");
            }
        }*/

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Book book)
        {
            try
            {
                if (book.Id != id)
                {
                    response.StatusCode = 401;
                               context.Fail("Invalid Data");
                               return Task.CompletedTask;
                    //return BadRequest("Dados inválidos");
                }
                await _bookService.UpdateBook(book);
                    return Ok($"livro com id = {id} foi atualizado com sucesso");
                
            }
            catch
            {

                return BadRequest("Inválid Request");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var book = await _bookService.GetBookById(id);
                if (book == null)
                {
                    return NotFound($"livro com id = {id} não foi encontrado");
                }

                await _bookService.DeleteBookById(book);
                    return Ok($"livro com id = {id} foi excluído com sucesso");
            }
            catch
            {

                return BadRequest("Inválid Request");
            }
        }

    }
}
