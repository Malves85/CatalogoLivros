using FluentValidation;
using CatalogoLivros.Entity;

namespace CatalogoLivros.Models.Books
{
    public class CreateBook
    {
        public long Isbn { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public decimal Price { get; set; }

        public Book ToEntity()
        {
            var book = new Book();
            book.Isbn = Isbn;
            book.Title = Title;
            book.AuthorId = AuthorId;
            book.Price = Price;

            return book;
        }
    }
    public class CreateBookValidator : AbstractValidator<CreateBook>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Isbn).NotNull().WithMessage("Insira o isbn").GreaterThanOrEqualTo(0).WithMessage("Insira um valor superior ou igual a 0 ").NotEmpty().WithMessage("Favor preencher o campo Isbn");
            RuleFor(x => x.Title).NotNull().WithMessage("Insira o título do livro").NotEmpty().WithMessage("Favor preencher o campo Title");
            //RuleFor(x => x.Author).NotNull().WithMessage("Insira o autor").NotEmpty().WithMessage("Favor preencher o campo Author");
            RuleFor(x => x.Price).NotNull().WithMessage("Insira o preço").GreaterThanOrEqualTo(0).WithMessage("O preço deve ser superior a 0 ").NotEmpty().WithMessage("Favor preencher o campo Price");
        }
    }
}