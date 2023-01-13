using CatalogoLivros.Context;
using CatalogoLivros.Helpers;
using CatalogoLivros.Infrastructure.Models.Authors;
using CatalogoLivros.Interface.Repositories;
using CatalogoLivros.Interface.Services;
using CatalogoLivros.Models.Authors;
using CatalogoLivros.Models.Books;

namespace CatalogoLivros.Services.Authors
{
    public class AuthorsService : IAuthorsService
    {
        private readonly AppDbContext _context;
        private IAuthorsService _authorsService;
        private IAuthorsRepository _authorsRepository;

        public AuthorsService(IAuthorsRepository authorsRepository, AppDbContext context)
        {
            _context = context;
            _authorsRepository = authorsRepository;
        }

        public async Task<MessagingHelper> DeleteAuthor(DeleteAuthor deleteAuthor)
        {
            MessagingHelper result = new();
            try
            {
                var responseRepository = await _authorsRepository.GetById(deleteAuthor.Id);
                if (responseRepository == null)
                {
                    result.Success = false;
                    result.Message = "Não foi possivel encontrar este autor";
                    return result;
                }

                //_context.Entry(responseRepository).CurrentValues["isDeleted"] = true;
                responseRepository.DeleteAuthor();
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Autor deletado com sucesso";
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocorreu um erro ao ir buscar autor";
            }

            return result;
        }


        //UpDate authors
        public async Task<MessagingHelper<AuthorDTO>> Update(EditAuthor editAuthor)
        {
            MessagingHelper<AuthorDTO> result = new();
            try
            {
                EditAuthorValidator validator = new();
                var responseValidator = validator.Validate(editAuthor);
                if (responseValidator.IsValid == false)
                {
                    result.Success = false;
                    result.Message = responseValidator.Errors.FirstOrDefault().ErrorMessage;
                    return result;
                }
                var authorDB = await _authorsRepository.GetById(editAuthor.Id);
                if (authorDB == null)
                {
                    result.Message = "Este autor não existe";
                    return result;
                }
                if (editAuthor.Name == authorDB.Name && editAuthor.Nacionality == authorDB.Nacionality && editAuthor.Image == authorDB.image)

                {
                    result.Success = false;
                    result.Message = "Nenhuma alteração feita";
                    return result;
                }

                authorDB.Name = editAuthor.Name;
                authorDB.Nacionality = editAuthor.Nacionality;
                authorDB.image = editAuthor.Image;

                var authorUpDate = await _authorsRepository.Update(authorDB);

                result.Success = true;
                result.Message = "Autor editado com sucesso";
                result.Obj = new AuthorDTO(authorUpDate);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Erro ao editar o autor";
            }

            return result;
        }


        // Get all authors
        public async Task<PaginatedList<ListAuthor>> GetAuthors(Search search)
        {
            PaginatedList<ListAuthor> response = new();

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

                var responseRepository = await _authorsRepository.GetAuthors(search.Sorting, search.Searching, search.CurrentPage, search.PageSize);
                if (responseRepository.Success != true)
                {
                    response.Success = false;
                    response.Message = "Erro ao obter a informação do livro";
                    return response;
                }

                response.Items = responseRepository.Items.Select(t => new ListAuthor(t)).ToList();
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

        // Create authors
        public async Task<MessagingHelper<int>> Create(CreateAuthor createAuthor)
        {
            MessagingHelper<int> response = new();

            try
            {
                var responseValidate = await new CreateAuthorValidator().ValidateAsync(createAuthor);
                if (responseValidate == null || responseValidate.IsValid == false)
                {
                    response.Message = responseValidate == null ? "Erro ao validar a informação para criar um livro" : responseValidate.Errors.FirstOrDefault()!.ErrorMessage;
                    response.Success = false;
                    return response;
                }

                var newAuthor = createAuthor.ToEntity();
                var authorInDB = await _authorsRepository.Create(newAuthor);
                if (authorInDB == null)
                {
                    response.Message = "Erro ao criar o autor!";
                    return response;
                }
                response.Success = true;
                response.Obj = authorInDB.Id;
                response.Message = "Autor criado com sucesso";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro inesperado ao criar o autor";
            }

            return response;
        }

    }
}
