using System.ComponentModel.DataAnnotations;
namespace MyLibrary.Models;

public class BookSet
{
    [Key]
    public int BookSetId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    // Navigation property
    public virtual ICollection<Book> Books { get; set; }
}

