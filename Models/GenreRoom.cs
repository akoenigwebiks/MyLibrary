using System.ComponentModel.DataAnnotations;
namespace MyLibrary.Models;

public class Library
{
    [Key]
    public int LibraryId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Type { get; set; }

    public string Location { get; set; }

    // Navigation property
    public virtual ICollection<Shelf> Shelves { get; set; }
}
