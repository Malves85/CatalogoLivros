
namespace CatalogoLivros.Models
{
    public class ListBook
    {
        public int Id { get; set; }
        public long Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public bool isDeleted { get; set; }

        public ListBook(Book book)
        {
            Id = book.Id;
            Isbn = book.Isbn;
            Title = book.Title;
            Author = book.Author;
            Price = book.Price;
            isDeleted = book.isDeleted;

        }
    }
    
}
