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
}

app.Run();