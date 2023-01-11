using CatalogoLivros.Entity;
using CatalogoLivros.Models.Books;
using FluentValidation;

namespace CatalogoLivros.Models.Authors
{
    public class CreateAuthor
    {
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public string Image { get; set; }
        public Author ToEntity()
        {
            var author = new Author();
            author.Name = Name;
            author.image = Image;
            author.Nacionality = Nacionality;
            return author;
        }
    }
    public class CreateAuthorValidator : AbstractValidator<CreateAuthor>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Insira o nome").NotEmpty().WithMessage("Favor preencher o campo Nome");
            RuleFor(x => x.Nacionality).NotNull().WithMessage("Insira o país").NotEmpty().WithMessage("Favor preencher o campo País");
        }
    }
}
