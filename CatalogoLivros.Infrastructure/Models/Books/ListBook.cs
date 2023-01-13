using CatalogoLivros.Entity;

namespace CatalogoLivros.Models.Books
{
    public class ListBook
    {
        public int Id { get; set; }
        public long Isbn { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public bool isDeleted { get; set; }
        public string image { get; set; }

        public ListBook(Book book)
        {
            Id = book.Id;
            Isbn = book.Isbn;
            Title = book.Title;
            AuthorId = book.AuthorId;
            AuthorName = book.Author.Name;
            Price = book.Price;
            isDeleted = book.isDeleted;
            image = book.image;

        }
    }

}
