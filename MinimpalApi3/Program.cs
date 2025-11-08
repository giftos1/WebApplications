using MinimalApiApp3;
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
}).AddEndpointFilter(ValidationHelper.ValidateId)
.AddEndpointFilter(async (context, next) =>
{
    app.Logger.LogInformation("Executing filter...");
    object? result = await next(context);
    app.Logger.LogInformation($"Handler result: {result}");
    return result;
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

/*A filter factory is a method that returns a filter function
ASP.NET Core executes the filter factory when it’s building your app and incorporates the returned filter into the filter pipeline for the app
You can use the same filterfactory function to emit a different filter for each endpoint, with each filter tailored to the endpoint’s parameters
*/

/*TIP Where possible, consider using middleware for cross - cutting
concerns.Use filters when you need different behavior for different
endpoints or where the functionality relies on endpoint concepts such
as IResult objects.*/