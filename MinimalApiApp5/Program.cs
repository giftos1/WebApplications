using MinimalApiApp5;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(); // Adds the IProblemDetailsService implementation
var app = builder.Build();
app.UseStatusCodePages();

var _fruit = new ConcurrentDictionary<string, Fruit>();
RouteGroupBuilder fruitApi = app.MapGroup("/fruit");

RouteGroupBuilder fruitWithApiValidation = fruitApi.MapGroup("/").AddEndpointFilter(ValidationHelper.ValidateIdFactory);

fruitApi.MapGet("/", () =>
{
    return _fruit;
});

fruitWithApiValidation.MapGet("/{id}", (string id) => {
    return _fruit.TryGetValue(id, out var fruit) ? TypedResults.Ok(fruit) : Results.Problem(statusCode: 404);
});

fruitWithApiValidation.MapPost("/{id}", (string id, Fruit fruit) =>
{
    return _fruit.TryAdd(id, fruit) ? TypedResults.Created($"/fruit/{id}") : Results.ValidationProblem(new Dictionary<string, string[]>
    {
        {"id", new[] {"A fruit with that id already exists"} }
    });
});

fruitWithApiValidation.MapPut("/{id}", (string id, Fruit fruit) =>
{
    _fruit[id] = fruit;
    return Results.NoContent();

});

fruitWithApiValidation.MapDelete("/fruit/{id}", (string id) =>
{
    return _fruit.TryRemove(id, out _) ? Results.NoContent() : Results.Problem(statusCode: 404);
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(); // Configure the ExceptionHandlerMiddleware without a path so that it uses the IProblemDetailsService
}


app.Run();

record Fruit(string Name, int Stock)
{
    public static readonly Dictionary<string, Fruit> All = new();
}
