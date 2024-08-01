using MyLibrary.Models;

namespace MyLibrary.ViewModels
{
    /// <summary>
    /// A modelrepresenting extra data for a shelf
    /// about the number of books it contains and the free space it has
    /// </summary>
    public class VMShelfFreeSpaceAndCount
    {
        public Shelf Shelf { get; set; }
        public int BookCount { get; set; }
        public double FreeWidth { get; set; }
    }
}
