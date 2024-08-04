using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MyLibrary.Models;

[Index(nameof(Name), IsUnique = true)]
public class Library
{
    [Key]
    public int LibraryId { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation property
    public virtual ICollection<Shelf> Shelves { get; set; }
}
