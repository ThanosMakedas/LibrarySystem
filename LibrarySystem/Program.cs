using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

var library = new Library();

// Seed data
SeedData(library);

bool running = true;

while (running)
{
    Console.WriteLine("\n=== Bibliotekssystem ===");
    Console.WriteLine("1. Visa alla böcker");
    Console.WriteLine("2. Sök bok");
    Console.WriteLine("3. Låna bok");
    Console.WriteLine("4. Returnera bok");
    Console.WriteLine("5. Visa medlemmar");
    Console.WriteLine("6. Statistik");
    Console.WriteLine("0. Avsluta");
    Console.Write("Välj: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowAllBooks(library);
            break;
        case "2":
            SearchBooks(library);
            break;
        case "3":
            BorrowBook(library);
            break;
        case "4":
            ReturnBook(library);
            break;
        case "5":
            ShowMembers(library);
            break;
        case "6":
            ShowStatistics(library);
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Ogiltigt val.");
            break;
    }
}

static void SeedData(Library library)
{
    library.BookCatalog.AddBook(new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954));
    library.BookCatalog.AddBook(new Book("978-91-0-065432-1", "Hobbiten", "J.R.R. Tolkien", 1937));
    library.BookCatalog.AddBook(new Book("978-91-0-111111-1", "Harry Potter", "J.K. Rowling", 1997));

    library.MemberRegistry.AddMember(new Member("M001", "Anna Andersson", "anna@test.se"));
    library.MemberRegistry.AddMember(new Member("M002", "Erik Svensson", "erik@test.se"));
}

static void ShowAllBooks(Library library)
{
    foreach (var book in library.BookCatalog.Books)
    {
        Console.WriteLine(book.GetInfo());
    }
}

static void SearchBooks(Library library)
{
    Console.Write("Sökterm: ");
    var term = Console.ReadLine();

    var results = library.BookCatalog.Search(term ?? "");

    Console.WriteLine("\nSökresultat:");
    foreach (var book in results)
    {
        Console.WriteLine(book.GetInfo());
    }
}

static void BorrowBook(Library library)
{
    Console.Write("Ange ISBN: ");
    var isbn = Console.ReadLine();

    Console.Write("Ange medlems-ID: ");
    var memberId = Console.ReadLine();

    var book = library.BookCatalog.GetByIsbn(isbn ?? "");
    var member = library.MemberRegistry.GetById(memberId ?? "");

    if (book == null || member == null)
    {
        Console.WriteLine("Felaktigt ISBN eller medlems-ID.");
        return;
    }

    var loan = library.LoanManager.BorrowBook(book, member);

    if (loan == null)
    {
        Console.WriteLine("Boken är inte tillgänglig.");
        return;
    }

    Console.WriteLine($"Boken \"{book.Title}\" har lånats ut till {member.Name}.");
    Console.WriteLine($"Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
}

static void ReturnBook(Library library)
{
    Console.Write("Ange ISBN: ");
    var isbn = Console.ReadLine();

    var loan = library.LoanManager.Loans
        .FirstOrDefault(l => l.Book.ISBN == isbn && !l.IsReturned);

    if (loan == null)
    {
        Console.WriteLine("Ingen aktiv utlåning hittades.");
        return;
    }

    library.LoanManager.ReturnBook(loan);
    Console.WriteLine("Boken har återlämnats.");
}

static void ShowMembers(Library library)
{
    foreach (var member in library.MemberRegistry.Members)
    {
        Console.WriteLine(member.GetMemberInfo());
    }
}

static void ShowStatistics(Library library)
{
    Console.WriteLine($"Totalt antal böcker: {library.BookCatalog.GetTotalBooks()}");
    Console.WriteLine($"Antal utlånade böcker: {library.BookCatalog.GetBorrowedBooksCount()}");

    var mostActive = library.MemberRegistry.GetMostActiveBorrower();

    if (mostActive != null)
        Console.WriteLine($"Mest aktiva låntagare: {mostActive.Name}");
}