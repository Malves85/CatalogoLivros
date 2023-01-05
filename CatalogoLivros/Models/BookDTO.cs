using CatalogoLivros.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoLivros.Models
{
    public class BookDTO
    {
        public long Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

        public BookDTO(Book book)
        {
            this.Isbn = book.Isbn;
            this.Title = book.Title;
            this.Author = book.Author;
            this.Price = book.Price;
        }
    }
}
