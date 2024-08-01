namespace MyLibrary.ViewModels;

public class VMAddBook
{
    public List<VMLibraryWithShelves> LibrariesWithShelves { get; set; }
    public string Title { get; set; }
    public decimal Height { get; set; }
    public decimal Thickness { get; set; }
    public int? ShelfId { get; set; } // Bind the selected ShelfId
}
