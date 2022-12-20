using System.ComponentModel;
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
        [Range( 0, 999, ErrorMessage = "Price must be >= 0")]
        public double Price { get; set; }
        [DefaultValue(false)]
        public bool isDeleted { get; set; }

    }
}

