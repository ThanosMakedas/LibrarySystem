using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests;

public class SearchTests
{
    [Theory]
    [InlineData("Tolkien", true)]
    [InlineData("tolkien", true)]   // case-insensitive
    [InlineData("Rowling", false)]
    public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
    {
        // Arrange
        var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

        // Act
        var result = book.Matches(searchTerm);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Book_Matches_ShouldFindByTitle()
    {
        // Arrange
        var book = new Book("123", "Hobbiten", "J.R.R. Tolkien", 1937);

        // Act
        var result = book.Matches("Hobbiten");

        // Assert
        Assert.True(result);
    }
}