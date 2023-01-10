using FluentValidation;

namespace CatalogoLivros.Models.Authors
{
    public class EditAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public string Image { get; set; }
    }
    public class EditAuthorValidator : AbstractValidator<EditAuthor>
    {
        public EditAuthorValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Insira o nome do autor").NotEmpty().WithMessage("Favor preencher o campo Nome");
            RuleFor(x => x.Nacionality).NotNull().WithMessage("Insira o nacionalidade").NotEmpty().WithMessage("Favor preencher o campo Nacionalidade");
            RuleFor(x => x.Name).NotNull().WithMessage("Insira o link da imagem do autor").NotEmpty().WithMessage("Favor preencher o campo Imagem");

        }
    }
}
