using Microsoft.EntityFrameworkCore;
using BookstoreAPI;
using BookstoreAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "..", "Bookstore.sqlite");
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.UseCors();

// GET /api/categories — distinct book categories for filtering
app.MapGet("/api/categories", async (BookstoreContext db) =>
{
    var categories = await db.Books
        .AsNoTracking()
        .Where(b => b.Category != null && b.Category != string.Empty)
        .Select(b => b.Category)
        .Distinct()
        .OrderBy(c => c)
        .ToListAsync();

    return Results.Ok(categories);
})
.WithName("GetCategories");

// GET /api/books?page=1&pageSize=5&sortBy=title&category=Biography
app.MapGet("/api/books", async (
    BookstoreContext db,
    int page = 1,
    int pageSize = 5,
    string sortBy = "title",
    string? category = null) =>
{
    var query = db.Books.AsNoTracking().AsQueryable();

    if (!string.IsNullOrWhiteSpace(category))
        query = query.Where(b => b.Category == category);

    query = sortBy.ToLowerInvariant() switch
    {
        "title" or "title_asc" => query.OrderBy(b => b.Title),
        "title_desc" => query.OrderByDescending(b => b.Title),
        _ => query.OrderBy(b => b.Title)
    };

    var totalCount = await query.CountAsync();
    var books = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return Results.Ok(new
    {
        books,
        totalCount,
        page,
        pageSize,
        totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
    });
})
.WithName("GetBooks");

app.Run();
