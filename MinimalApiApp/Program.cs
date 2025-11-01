using MinimalApiApp;

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
app.MapGet("/person/{name}", (string name) =>
{
    return persons.Where(p => p.FirstName.StartsWith(name, StringComparison.OrdinalIgnoreCase));
});

app.MapGet("/", () => persons.ToList());
//app.MapGet("/", () => persons.Select(p => $"{p.FirstName}, {p.LastName}"));

app.Run();

// You should only use GET verbs to get data from the server and never use it to send data on the server.
// Use POST or DELETE