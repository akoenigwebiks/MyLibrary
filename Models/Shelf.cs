using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MyLibrary.Models;

public class Shelf
{
    [Key]
    public int ShelfId { get; set; }

    [Required]
    public int LibraryId { get; set; }

    public string Number { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }

    // Navigation properties
    [ForeignKey("LibraryId")]
    public virtual Library Library { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}