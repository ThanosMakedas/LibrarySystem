using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests;

public class BookTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange & Act
        var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

        // Assert
        Assert.Equal("978-91-0-012345-6", book.ISBN);
        Assert.Equal("Testbok", book.Title);
        Assert.Equal("Testförfattare", book.Author);
        Assert.Equal(2024, book.PublishedYear);
        Assert.True(book.IsAvailable);
    }

    [Fact]
    public void IsAvailable_ShouldBeTrueForNewBook()
    {
        // Arrange
        var book = new Book("123", "Test", "Author", 2024);

        // Act & Assert
        Assert.True(book.IsAvailable);
    }

    [Fact]
    public void GetInfo_ShouldReturnFormattedString()
    {
        // Arrange
        var book = new Book("123", "Testbok", "Testförfattare", 2024);

        // Act
        var info = book.GetInfo();

        // Assert
        Assert.Contains("Testbok", info);
        Assert.Contains("Testförfattare", info);
        Assert.Contains("2024", info);
        Assert.Contains("Tillgänglig", info);
    }
}
