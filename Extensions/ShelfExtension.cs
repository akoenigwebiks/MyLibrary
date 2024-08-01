using MyLibrary.Models;

namespace MyLibrary.Extensions;

public static class ShelfExtension
{
    public static double FreeSpace(this Shelf shelf) => (double)(shelf.Width - shelf.Books.Sum(b => b.Thickness));

    /// <summary>
    /// a method that takes in this shelf and book 
    /// and returns a boolean indicating if the shelf has enough space for the book 
    /// </summary>
    /// <param name="shelf"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    public static bool HasEnoughSpace(this Shelf shelf, Book book)
    {
        if (book.Height > shelf.Height)
        {
            return false;
        }

        var totalThickness = shelf.FreeSpace() + (double)book.Thickness;
        return totalThickness <= (double)shelf.Width;
    }
}

