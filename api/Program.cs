using Microsoft.EntityFrameworkCore;
using PurchasesApi.Data;
using PurchasesApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

// The video uses SQL Server. Set "Database:Provider" to "Sqlite" to run without installing SQL Server.
var provider = builder.Configuration["Database:Provider"] ?? "SqlServer";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

// Creates the database with the demo data (5 users, 4 purchases) the first time it runs.
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
}

// The endpoint from the video.
app.MapGet("/api/users/purchases", async (AppDbContext db) =>
{
    var users = await db.Users
        .OrderBy(u => u.Id) // not shown in the video: keeps the output order stable
        .Select(u => new UserPurchasesDto(
            u.Id,
            u.Name,
            u.Purchases
                .Select(p => new PurchaseDto(
                    p.Id,
                    p.Product,
                    p.Amount))
                .ToList()
        ))
        .ToListAsync();

    return Results.Ok(users);
});

app.Run();
