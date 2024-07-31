namespace MyLibrary.Models;
public class Shelf
{
    public int Id { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int GenreRoomId { get; set; }
    public virtual GenreRoom GenreRoom { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}
