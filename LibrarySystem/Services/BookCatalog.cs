using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class BookCatalog
{
    private readonly List<Book> _books = new();

    public IReadOnlyList<Book> Books => _books;

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Book? GetByIsbn(string isbn)
    {
        return _books.FirstOrDefault(b => b.ISBN == isbn);
    }

    public List<Book> Search(string searchTerm)
    {
        return _books.Where(b => b.Matches(searchTerm)).ToList();
    }

    public List<Book> SortByTitle()
    {
        return _books.OrderBy(b => b.Title).ToList();
    }

    public List<Book> SortByYear()
    {
        return _books.OrderBy(b => b.PublishedYear).ToList();
    }

    public int GetTotalBooks()
    {
        return _books.Count;
    }

    public int GetBorrowedBooksCount()
    {
        return _books.Count(b => !b.IsAvailable);
    }
}
