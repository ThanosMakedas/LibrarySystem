using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Models;

public class Member
{
    public string MemberId { get; }            
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime MemberSince { get; }

    // Properties för att hålla reda på lånade böcker
    private readonly List<Book> _borrowedBooks = new();
    public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks;

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