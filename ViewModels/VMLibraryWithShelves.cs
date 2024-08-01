using MyLibrary.Models;

namespace MyLibrary.ViewModels;

public class VMLibraryWithShelves
{
    public int LibraryId { get; set; }
    public string Name { get; set; }
    public List<VMShelfFreeSpaceAndCount> VMShelfFreeSpaceAndCount { get; set; }
}
