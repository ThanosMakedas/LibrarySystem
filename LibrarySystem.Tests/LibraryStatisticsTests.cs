using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

namespace LibrarySystem.Tests;

public class LibraryStatisticsTests
{
    [Fact]
    public void GetTotalBooks_ShouldReturnCorrectCount()
    {
        // Arrange
        var library = new Library();
        library.BookCatalog.AddBook(new Book("1", "A", "Author", 2000));
        library.BookCatalog.AddBook(new Book("2", "B", "Author", 2001));

        // Act
        var total = library.BookCatalog.GetTotalBooks();

        // Assert
        Assert.Equal(2, total);
    }

    [Fact]
    public void GetBorrowedBooksCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var library = new Library();

        var book1 = new Book("1", "A", "Author", 2000);
        var book2 = new Book("2", "B", "Author", 2001);
        var member = new Member("M1", "Test", "test@test.com");

        library.BookCatalog.AddBook(book1);
        library.BookCatalog.AddBook(book2);
        library.MemberRegistry.AddMember(member);

        library.LoanManager.BorrowBook(book1, member);

        // Act
        var borrowedCount = library.BookCatalog.GetBorrowedBooksCount();

        // Assert
        Assert.Equal(1, borrowedCount);
    }

    [Fact]
    public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
    {
        // Arrange
        var library = new Library();

        var book1 = new Book("1", "A", "Author", 2000);
        var book2 = new Book("2", "B", "Author", 2001);

        var member1 = new Member("M1", "Anna", "anna@test.com");
        var member2 = new Member("M2", "Erik", "erik@test.com");

        library.BookCatalog.AddBook(book1);
        library.BookCatalog.AddBook(book2);

        library.MemberRegistry.AddMember(member1);
        library.MemberRegistry.AddMember(member2);

        library.LoanManager.BorrowBook(book1, member1);
        library.LoanManager.BorrowBook(book2, member1);

        // Act
        var mostActive = library.MemberRegistry.GetMostActiveBorrower();

        // Assert
        Assert.NotNull(mostActive);
        Assert.Equal("Anna", mostActive!.Name);
    }

    [Fact]
    public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
    {
        // Arrange
        var library = new Library();

        var book1 = new Book("1", "Zebra", "Author", 2000);
        var book2 = new Book("2", "Alpha", "Author", 2001);

        library.BookCatalog.AddBook(book1);
        library.BookCatalog.AddBook(book2);

        // Act
        var sorted = library.BookCatalog.SortByTitle();

        // Assert
        Assert.Equal("Alpha", sorted[0].Title);
        Assert.Equal("Zebra", sorted[1].Title);
    }
}