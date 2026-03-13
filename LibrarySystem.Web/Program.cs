using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using LibrarySystem.Web.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibrarySystemDB;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();

    if (!db.Books.Any())
    {
        db.Books.AddRange(
            new Book
            {
                ISBN = "9780131103627",
                Title = "The C Programming Language",
                Author = "Kernighan & Ritchie",
                PublishedYear = 1988,
                IsAvailable = true
            },
            new Book
            {
                ISBN = "9780201633610",
                Title = "Design Patterns",
                Author = "Gamma et al.",
                PublishedYear = 1994,
                IsAvailable = true
            },
            new Book
            {
                ISBN = "9780132350884",
                Title = "Clean Code",
                Author = "Robert C. Martin",
                PublishedYear = 2008,
                IsAvailable = true
            }
        );

        db.SaveChanges();
    }

    if (!db.Members.Any())
    {
        db.Members.AddRange(
            new Member
            {
                MemberId = "M001",
                Name = "Anna Svensson",
                Email = "anna@example.com",
                MemberSince = DateTime.Now.AddMonths(-6)
            },
            new Member
            {
                MemberId = "M002",
                Name = "Erik Johansson",
                Email = "erik@example.com",
                MemberSince = DateTime.Now.AddMonths(-3)
            }
        );

        db.SaveChanges();
    }

    if (!db.Loans.Any())
    {
        var firstBook = db.Books.FirstOrDefault();
        var firstMember = db.Members.FirstOrDefault();

        if (firstBook != null && firstMember != null)
        {
            firstBook.IsAvailable = false;

            db.Loans.Add(new Loan
            {
                BookId = firstBook.Id,
                MemberId = firstMember.Id,
                LoanDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = null
            });

            db.SaveChanges();
        }
    }
}

app.Run();