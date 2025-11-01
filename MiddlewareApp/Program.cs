var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseWelcomePage("/");
// Middleware is added with Use* methods
// MapGet defines an endpoint
// WebApplication automatically adds some Middleware to the pipeline such as EndPointMiddleware in the Development Environment
// The order of the Use* methods matters as it defines the order of the middleware pipeline
//app.UseDeveloperExceptionPage(); // not necessary as its already added by WebApplication by default
app.UseStaticFiles();
app.UseRouting(); // this overrides the default routing middleware location that was added by WebApplication

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// additional middleware configuration
app.MapGet("/error", () => "An error has occurred while processiong your request");

app.MapGet("/", () => "Hello World!");

app.Run();

/*
- Middleware has a similar role to HTTP modules and
handlers in ASP.NET but is easier to reason about.
- Middleware is composed in a pipeline, with the
output of one middleware passing to the input of
the next.
- The middleware pipeline is two-way: requests pass
through each middleware on the way in, and
responses pass back through in reverse order on
the way out.
- Middleware can short-circuit the pipeline by
handling a request and returning a response, or it
can pass the request on to the next middleware in
the pipeline.
- Middleware can modify a request by adding data to
or changing the HttpContext object.
- If an earlier middleware short-circuits the pipeline,
not all middleware will execute for all requests.
- If a request isn’t handled, the middleware pipeline
returns a 404 status code.
- The order in which middleware is added to
WebApplication defines the order in which
middleware will execute in the pipeline.
- The middleware pipeline can be reexecuted as long
as a response’s headers haven’t been sent.
- When it’s added to a middleware pipeline,
StaticFileMiddleware serves any requested
files found in the wwwroot folder of your
application.
- DeveloperExceptionPageMiddleware
provides a lot of information about errors during
development, but it should never be used in
production.
- ExceptionHandlerMiddleware lets you
provide user-friendly custom error-handling
messages when an exception occurs in the
pipeline. It’s safe for use in production, as it
doesn’t expose sensitive details about your
application.
- Microsoft provides some common middleware, and
many third-party options are available on NuGet and Github
*/
