using JobTracker;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var applications = new List<Application>();

app.MapGet("/applications", () => applications);

app.MapGet("/applications/{id}", (int id) =>
{
    var application = applications.FirstOrDefault(a => a.Id == id);
    return application is not null ? Results.Ok(application) : Results.NotFound();
});

app.MapPost("/applications", (Application input) =>
{
    input.Id = applications.Count > 0 ? applications.Max(a => a.Id) + 1 : 1;
    applications.Add(input);
    return Results.Created($"/applications/{input.Id}", input);
});

app.MapPut("/applications/{id}", (int id, Application input) =>
{
    var application = applications.FirstOrDefault(a => a.Id == id);
    if (application == null) return Results.NotFound();

    application.Company = input.Company;
    application.Role = input.Role;
    application.Status = input.Status;
    application.DateApplied = input.DateApplied;
    application.Notes = input.Notes;

    return Results.NoContent();
});

app.MapDelete("/applications/{id}", (int id) =>
{
    var application = applications.FirstOrDefault(a => a.Id == id);
    if (application == null) return Results.NotFound();

    applications.Remove(application);
    return Results.NoContent();
});

app.Run();
