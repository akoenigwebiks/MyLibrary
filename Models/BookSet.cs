namespace MyLibrary.Models;
public class BookSet
{
    public int Id { get; set; }
    public int GenreRoomId { get; set; }
    public virtual GenreRoom GenreRoom { get; set; }
    public virtual ICollection<Book> Books { get; set; }
    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; }
}

