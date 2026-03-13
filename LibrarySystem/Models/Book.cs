using LibrarySystem.Core.Interfaces;

namespace LibrarySystem.Core.Models;

public class Book : ISearchable
{
    public int Id { get; set; }

    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public bool IsAvailable { get; set; } = true;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public Book()
    {
    }

    public Book(string isbn, string title, string author, int publishedYear)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        PublishedYear = publishedYear;
        IsAvailable = true;
    }

    public string GetInfo()
    {
        var status = IsAvailable ? "Tillgänglig" : "Utlånad";
        return $"\"{Title}\" av {Author} ({PublishedYear}) - {status}";
    }

    public void MarkAsBorrowed()
    {
        IsAvailable = false;
    }

    public void MarkAsReturned()
    {
        IsAvailable = true;
    }

    public bool Matches(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return false;

        searchTerm = searchTerm.Trim();

        return ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
    }
}