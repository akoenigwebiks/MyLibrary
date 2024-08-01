using MyLibrary.Models;

namespace MyLibrary.ViewModels;

public class LibraryWithShelves
{
    public int LibraryId { get; set; }
    public string Name { get; set; }
    public List<Shelf> Shelves { get; set; }
}

public class VMAddBook
{
    public List<LibraryWithShelves> LibrariesWithShelves { get; set; }
    public string Title { get; set; }
    public decimal Height { get; set; }
    public decimal Thickness { get; set; }
    public int? ShelfId { get; set; } // Bind the selected ShelfId
}
