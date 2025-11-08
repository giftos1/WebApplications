using MinimalApiApp4;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(); // Adds the IProblemDetailsService implementation
var app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(); // Configure the ExceptionHandlerMiddleware without a path so that it uses the IProblemDetailsService
}

// The StatusCodePagesMiddleware reexcutes the middleware pipeline with an error handling path
// as you can with the ExceptionHandlerMiddleware (useful with Razor Pages)
// It ensures that your API returns a ProblemDetails response for all error responses

app.UseStatusCodePages();

app.MapGet("/fruit", () =>
{
    return _fruit;
});


app.MapGet("/fruit/{id}", (string id) =>
{
    return _fruit.TryGetValue(id, out var fruit) ? TypedResults.Ok(fruit) : Results.Problem(statusCode: 404);
}).AddEndpointFilter<IdValidationFilter>();
//.AddEndpointFilter(async (context, next) =>
//{
//    app.Logger.LogInformation("Executing filter...");
//    object? result = await next(context);
//    app.Logger.LogInformation($"Handler result: {result}");
//    return result;
//});

app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
{
    return _fruit.TryAdd(id, fruit) ? TypedResults.Created($"/fruit/{id}") : Results.ValidationProblem(new Dictionary<string, string[]>
    {
        {"id", new[] {"A fruit with that id already exists"} }
    });
}).AddEndpointFilter<IdValidationFilter>();

app.Run();

record Fruit(string Name, int Stock)
{
    public static readonly Dictionary<string, Fruit> All = new();
}
