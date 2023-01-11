using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoLivros.Entity
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
        [Range(0, 999, ErrorMessage = "Preço tem de ser maior que 0")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        [DefaultValue(false)]
        public bool isDeleted { get; set; }
        [DefaultValue("https://img.freepik.com/free-psd/book-hardcover-mockup-three-views_125540-226.jpg?size=626&ext=jpg&ga=GA1.2.560839453.1661523512&semt=sph")]
        public string? image { get; set; }
        
        public int AuthorId{ get; set; }
        
        public Author Author { get; set; } = null!;

    }

}

