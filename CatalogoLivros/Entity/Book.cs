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
        [StringLength(80)]
        public string Author { get; set; }
        [Required]
        [Range(0, 999, ErrorMessage = "Preço tem de ser maior que 0")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [DefaultValue(false)]
        public bool isDeleted { get; set; }

    }

}

