using CatalogoLivros.Entity;

namespace CatalogoLivros.Models.Authors
{
    public class ListAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public string Image { get; set; }
        public ListAuthor(Author author){
            Id = author.Id;
            Name = author.Name;
            Nacionality= author.Nacionality;
            Image = author.image;
        }
    }
}
