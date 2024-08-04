using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace MyLibrary.Models;

[Index(nameof(Name), IsUnique = true)]
public class GenreRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Shelf> Shelves { get; set; }
}

