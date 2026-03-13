using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Tests;

public class LibraryEfTests
{
    private LibraryContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new LibraryContext(options);
    }

    [Fact]
    public async Task AddBook_ShouldSaveBookToDatabase()
    {
        using var context = CreateContext(nameof(AddBook_ShouldSaveBookToDatabase));

        var book = new Book
        {
            ISBN = "111",
            Title = "Test Book",
            Author = "Test Author",
            PublishedYear = 2024,
            IsAvailable = true
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "111");

        Assert.NotNull(savedBook);
        Assert.Equal("Test Book", savedBook!.Title);
    }

    [Fact]
    public async Task GetAllBooks_ShouldReturnAllBooks()
    {
        using var context = CreateContext(nameof(GetAllBooks_ShouldReturnAllBooks));

        context.Books.AddRange(
            new Book { ISBN = "101", Title = "Book 1", Author = "Author 1", PublishedYear = 2020 },
            new Book { ISBN = "102", Title = "Book 2", Author = "Author 2", PublishedYear = 2021 }
        );

        await context.SaveChangesAsync();

        var books = await context.Books.ToListAsync();

        Assert.Equal(2, books.Count);
    }

    [Fact]
    public async Task UpdateBook_ShouldChangeTitle()
    {
        using var context = CreateContext(nameof(UpdateBook_ShouldChangeTitle));

        var book = new Book
        {
            ISBN = "222",
            Title = "Old Title",
            Author = "Author",
            PublishedYear = 2022
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        book.Title = "New Title";
        await context.SaveChangesAsync();

        var updatedBook = await context.Books.FirstAsync(b => b.ISBN == "222");

        Assert.Equal("New Title", updatedBook.Title);
    }

    [Fact]
    public async Task DeleteBook_ShouldRemoveBookFromDatabase()
    {
        using var context = CreateContext(nameof(DeleteBook_ShouldRemoveBookFromDatabase));

        var book = new Book
        {
            ISBN = "333",
            Title = "Delete Me",
            Author = "Author",
            PublishedYear = 2021
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        context.Books.Remove(book);
        await context.SaveChangesAsync();

        var deletedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "333");

        Assert.Null(deletedBook);
    }

    [Fact]
    public async Task AddMember_ShouldSaveMemberToDatabase()
    {
        using var context = CreateContext(nameof(AddMember_ShouldSaveMemberToDatabase));

        var member = new Member
        {
            MemberId = "M100",
            Name = "Anna Svensson",
            Email = "anna@test.com",
            MemberSince = DateTime.Now
        };

        context.Members.Add(member);
        await context.SaveChangesAsync();

        var savedMember = await context.Members.FirstOrDefaultAsync(m => m.MemberId == "M100");

        Assert.NotNull(savedMember);
        Assert.Equal("Anna Svensson", savedMember!.Name);
    }

    [Fact]
    public async Task CreateLoan_ShouldSaveLoan()
    {
        using var context = CreateContext(nameof(CreateLoan_ShouldSaveLoan));

        var book = new Book
        {
            ISBN = "501",
            Title = "Loan Book",
            Author = "Author",
            PublishedYear = 2023,
            IsAvailable = true
        };

        var member = new Member
        {
            MemberId = "M200",
            Name = "Erik Johansson",
            Email = "erik@test.com",
            MemberSince = DateTime.Now
        };

        context.Books.Add(book);
        context.Members.Add(member);

        await context.SaveChangesAsync();

        var loan = new Loan
        {
            BookId = book.Id,
            MemberId = member.Id,
            LoanDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14),
            ReturnDate = null
        };

        context.Loans.Add(loan);
        await context.SaveChangesAsync();

        var savedLoan = await context.Loans.FirstOrDefaultAsync();

        Assert.NotNull(savedLoan);
    }
}