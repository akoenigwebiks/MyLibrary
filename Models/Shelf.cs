using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace MyLibrary.Models;

[Index(nameof(Number), IsUnique = true)]
public class Shelf
{
    [Key]
    public int ShelfId { get; set; }

    [Required]
    public int LibraryId { get; set; }

    [Required]
    public string Number { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Height { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Width { get; set; }

    // Navigation properties
    [ForeignKey("LibraryId")]
    public virtual Library Library { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}