using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(); // Adds the IProblemDetailsService implementation
var app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();

//app.MapGet("/", () => "Hello World!");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(); // Configure the ExceptionHandlerMiddleware without a path so that it uses the IProblemDetailsService
}

app.MapGet("/", void () => throw new Exception()); // throw an exception to demonstrate the behavior


app.MapGet("/fruit", () =>
{
    return _fruit;
});


app.MapGet("/fruit/{id}", (string id) =>
{
    return _fruit.TryGetValue(id, out var fruit) ? TypedResults.Ok(fruit) : Results.Problem(statusCode: 404);
});

app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
{
    return _fruit.TryAdd(id, fruit) ? TypedResults.Created($"/fruit/{id}") : Results.ValidationProblem(new Dictionary<string, string[]>
    {
        {"id", new[] {"A fruit with that id already exists"} }
    });
});

app.Run();

record Fruit(string Name, int Stock)
{
    public static readonly Dictionary<string, Fruit> All = new();
}
