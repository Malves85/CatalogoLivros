using CatalogoLivros.Entity;

namespace CatalogoLivros.Models.Authors
{
    public class AuthorDTO
    {
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public string Image { get; set; }
        public AuthorDTO(Author author)
        {
            Name = author.Name;
            Nacionality= author.Nacionality;
            Image = author.image;
        }
    }
}
