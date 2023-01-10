using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatalogoLivros.Entity
{
    public class Author
    {

        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = null!;
        public string Nacionality { get; set; }
        public string? image { get; set; }
        [DefaultValue(false)]
        public bool isDeleted { get; set; }
    }
}
