using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class LoanManager
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans;

    public Loan? BorrowBook(Book book, Member member)
    {
        if (!book.IsAvailable)
            return null;

        var loan = new Loan(
            book,
            member,
            DateTime.Now,
            DateTime.Now.AddDays(14)
        );

        book.MarkAsBorrowed();
        member.AddBorrowedBook(book);

        _loans.Add(loan);

        return loan;
    }

    public void ReturnBook(Loan loan)
    {
        if (loan.IsReturned)
            return;

        loan.Return();
        loan.Book.MarkAsReturned();
        loan.Member.RemoveBorrowedBook(loan.Book);
    }
}
