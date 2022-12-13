using System.ComponentModel.DataAnnotations;

namespace CatalogoLivros.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long Isbn { get; set; }
        [Required]
        [StringLength(80)]
        public string Title { get; set; }
        [Required]
        [StringLength(80)]
        public string Author { get; set; }
        [Required]
        public double Price { get; set; }

    }
}

