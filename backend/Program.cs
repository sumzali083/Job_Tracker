using JobTracker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/applications", async (ApplicationDbContext db) =>
    await db.Applications.ToListAsync());

app.MapGet("/applications/{id}", async (int id, ApplicationDbContext db) =>
{
    var application = await db.Applications.FindAsync(id);
    return application is not null ? Results.Ok(application) : Results.NotFound();
});

app.MapPost("/applications", async (Application input, ApplicationDbContext db) =>
{
    db.Applications.Add(input);
    await db.SaveChangesAsync();
    return Results.Created($"/applications/{input.Id}", input);
});

app.MapPut("/applications/{id}", async (int id, Application input, ApplicationDbContext db) =>
{
    var application = await db.Applications.FindAsync(id);
    if (application == null) return Results.NotFound();

    application.Company = input.Company;
    application.Role = input.Role;
    application.Status = input.Status;
    application.DateApplied = input.DateApplied;
    application.Notes = input.Notes;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/applications/{id}", async (int id, ApplicationDbContext db) =>
{
    var application = await db.Applications.FindAsync(id);
    if (application == null) return Results.NotFound();

    db.Applications.Remove(application);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
