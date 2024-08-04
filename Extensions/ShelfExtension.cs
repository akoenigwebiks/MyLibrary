using MyLibrary.Models;

namespace MyLibrary.Extensions;

public static class ShelfExtension
{
    public static double FreeSpace(this Shelf shelf) => (double)(shelf.Width - shelf.Books.Sum(b => b.Thickness));

    // shelf.FreeSpace()
    // var exs = new ShelfExtension();
    // exs.FreeSpace(shelf);

    /// <summary>
    /// a method that takes in this shelf and book 
    /// and returns a boolean indicating if the shelf has enough space for the book 
    /// </summary>
    /// <param name="shelf"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    public static bool HasEnoughSpace(this Shelf shelf, Book book)
    {
        //if (book.Height > shelf.Height)
        //{
        //    return false;
        //}

        //return (double)book.Thickness <= shelf.FreeSpace();

        return shelf switch
        {
            _ when book.Height > shelf.Height => false,
            _ when (double)book.Thickness <= shelf.FreeSpace() => true,
            _ => false
        };
    }
}

