using MinimalApiApp;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var persons = new List<Person>
{
    new Person("Maximiliano", "Devora"),
    new Person("Leornado", "Davinci"),
    new Person("Lordmas", "Mawayne"),
    new Person("Commando", "Joker"),
    new Person("Constable", "Min"),
    new Person("Joker", "Smiles"),
    new Person("James", "Dogeston"),
    new Person("Wonder", "Man"),
    new Person("Watson", "Jacob"),
    new Person("Moses", "Dogman"),
};

// lambda expression
var getFruit = (string id) => Fruit.All[id];

// instance methods
Handlers handlers = new Handlers();
app.MapPut("/fruit/{id}", handlers.ReplaceFruit);

app.MapGet("/fruit/{id}", getFruit);

app.MapGet("/fruit", () => Fruit.All);
app.MapGet("/person/{name}", (string name) =>
{
    return persons.Where(p => p.FirstName.StartsWith(name, StringComparison.OrdinalIgnoreCase));
});

app.MapGet("/", () => persons.ToList());

app.MapPost("/fruit/{id}", Handlers.AddFruit);
app.MapDelete("/fruit/{id}", DeleteFruit);
app.Run();

// local function
void DeleteFruit(string id)
{
    Fruit.All.Remove(id);
}

record Fruit(string Name, int Stock)
{
    public static readonly Dictionary<string, Fruit> All = new();
}



// You should only use GET verbs to get data from the server and never use it to send data on the server.
// Use POST or DELETE
// JetBrains RIDER makes it easy to craft HTTP requests from inside your API, and even discovers all the endpoints in your application automatically
// complex types- Types that can't be extracted from the URL by means of route parameters - are created by deserializing the JSON body of a request

/*
- NOTE By contrast with APIs built using ASP.NET and ASP.NET Core
web API controllers, minimal APIs can
bind only to JSON bodies and always use the System.Text.Json
library for JSON deserialization.

- Sending a POST request with Postman. The minimal
API automatically deserializes the JSON in the request body to a
Fruit instance before calling the endpoint handler.

*/