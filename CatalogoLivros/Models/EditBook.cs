using FluentValidation;

namespace CatalogoLivros.Models
{
    public class EditBook
    {
        public int Id { get; set; }
        public long Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
    }
    public class EditBookValidator : AbstractValidator<EditBook>
    {
        public EditBookValidator()
        {
            RuleFor(x => x.Isbn).NotNull().WithMessage("Insira o isbn").GreaterThanOrEqualTo(0).WithMessage("Insira um valor superior ou igual a 0 ");
            RuleFor(x => x.Title).NotNull().WithMessage("Insira o título do livro");
            RuleFor(x => x.Author).NotNull().WithMessage("Insira o autor");
            RuleFor(x => x.Price).NotNull().WithMessage("Insira o preço").GreaterThanOrEqualTo(0).WithMessage("Insira um valor superior ou igual a 0 ");
        }
    }
}
