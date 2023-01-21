using CatalogoLivros.Helpers;
using CatalogoLivros.Infrastructure.Models.Authors;
using CatalogoLivros.Interface.Services;
using CatalogoLivros.Models.Authors;
using CatalogoLivros.Models.Books;
using CatalogoLivros.Services.Books;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }


        [HttpPost("getAuthors")]

        public async Task<PaginatedList<ListAuthor>> GetAuthors(Search search)
        {
            return await _authorsService.GetAuthors(search);
        }

        [HttpPost("create")]

        public async Task<MessagingHelper<int>> Create(CreateAuthor createAuthor)
        {
            return await _authorsService.Create(createAuthor);
        }
        [HttpPost("update")]
        public async Task<MessagingHelper<AuthorDTO>> Update(EditAuthor editAuthor)
        {
            return await _authorsService.Update(editAuthor);
        }

        [HttpGet("{id}")]
        public async Task<MessagingHelper<AuthorDTO>> GetById(int id)
        {

            return await _authorsService.GetById(id);
        }

        [HttpPost("delete")]
        public async Task<MessagingHelper> Delete(DeleteAuthor deleteAuthor)
        {
            return await _authorsService.DeleteAuthor(deleteAuthor);

        }
    }
}
