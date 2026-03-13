namespace LibrarySystem.Core.Models;

public class Member
{
    public int Id { get; set; }

    public string MemberId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime MemberSince { get; set; } = DateTime.Now;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    // Behåll från Del 1 så att gamla services fortfarande fungerar
    private readonly List<Book> _borrowedBooks = new();
    public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks;

    public Member()
    {
    }

    public Member(string memberId, string name, string email)
    {
        MemberId = memberId;
        Name = name;
        Email = email;
        MemberSince = DateTime.Now;
    }

    public void AddBorrowedBook(Book book)
    {
        _borrowedBooks.Add(book);
    }

    public void RemoveBorrowedBook(Book book)
    {
        _borrowedBooks.Remove(book);
    }

    public string GetMemberInfo()
    {
        return $"{MemberId} - {Name} ({Email}), medlem sedan {MemberSince:yyyy-MM-dd}. Lånade böcker: {_borrowedBooks.Count}";
    }
}