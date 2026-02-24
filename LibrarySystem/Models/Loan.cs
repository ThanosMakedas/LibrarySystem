namespace LibrarySystem.Core.Models;

public class Loan
{
    public Book Book { get; }
    public Member Member { get; }
    public DateTime LoanDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public bool IsOverdue
    {
        get
        {
            if (IsReturned) return false;
            return DateTime.Now.Date > DueDate.Date;
        }
    }

    public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
    {
        Book = book;
        Member = member;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = null;
    }

    public void Return()
    {
        ReturnDate = DateTime.Now;
    }
}