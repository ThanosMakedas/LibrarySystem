namespace LibrarySystem.Core.Models;

public class Loan
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = default!;

    public int MemberId { get; set; }
    public Member Member { get; set; } = default!;

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public bool IsReturned => ReturnDate.HasValue;

    public bool IsOverdue
    {
        get
        {
            if (IsReturned) return false;
            return DateTime.Now.Date > DueDate.Date;
        }
    }

    public Loan()
    {
    }

    public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
    {
        Book = book;
        BookId = book.Id;
        Member = member;
        MemberId = member.Id;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = null;
    }

    public void Return()
    {
        ReturnDate = DateTime.Now;
    }
}