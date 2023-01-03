using FluentValidation;
using CatalogoLivros.Models;

namespace CatalogoLivros.Models
{
    public class CreateBook
    {
        public int Id { get; set; }
        public long Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        
            public Book ToEntity()
        {
            var book = new Book();
            book.Id = this.Id;
            book.Isbn = this.Isbn;
            book.Title = this.Title;
            book.Author = this.Author;
            book.Price = this.Price;

            return book;
        }
    }
    public class CreateBookValidator : AbstractValidator<CreateBook>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Isbn).NotNull().WithMessage("Insira o isbn").GreaterThanOrEqualTo(0).WithMessage("Insira um valor superior ou igual a 0 "); //.Must(BeUniqueIsbn).WithMessage("Isbn já existe");
            RuleFor(x => x.Title).NotNull().WithMessage("Insira o título do livro");
            RuleFor(x => x.Author).NotNull().WithMessage("Insira o autor");
            RuleFor(x => x.Price).NotNull().WithMessage("Insira o preço").GreaterThanOrEqualTo(0).WithMessage("Insira um valor superior ou igual a 0 ");
        }
    }
}