using CatalogoLivros.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CatalogoLivros.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public long Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public BookDTO(Book book)
        {
            this.Id = book.Id;
            this.Isbn = book.Isbn;
            this.Title = book.Title;
            this.Author = book.Author;
            this.Price = book.Price;
        }
    }
}
