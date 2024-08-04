using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MyLibrary.Models;

public class Book
{
    [Key]
    public int BookId { get; set; }

    public int? ShelfId { get; set; }

    [Required]
    public string Title { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Height { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Thickness { get; set; }

    // Navigation properties
    [ForeignKey("ShelfId")]
    public virtual Shelf Shelf { get; set; }

    [ForeignKey("BookSetId")]
    public virtual BookSet? BookSet { get; set; }
}
