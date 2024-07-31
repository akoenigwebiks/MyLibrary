namespace MyLibrary.Models;
public class GenreRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Shelf> Shelves { get; set; }
}

