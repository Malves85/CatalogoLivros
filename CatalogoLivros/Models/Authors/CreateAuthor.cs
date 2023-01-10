using CatalogoLivros.Entity;

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
}
