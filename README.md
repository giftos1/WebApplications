# WebApplications

## WebApp1 
- An intro to Asp.Net Core Web Applications and its structure.
## MiddleWareAPP 
- Touches on Different Middlewares
## MinimalApiApp 
- Uses TypedResults and Results to return responses from a minimal API.
## MinimalApiApp2 
- Uses Problem and ValidationProblem to return error responses from a minimal API.
## MinimalApiApp3 
- Uses EndpointFilterFactory to endpoint filters which allow endpoint arguments not to be called in a specific order.
## MinimalApiApp4 
- Uses IEndpointFilter for applying endpoint filters in a minimal API.
## MinimalApiApp5 
- Uses MapGroups to group endpoints in a minimal API.

# Summary
- HTTP verbs define the semantic expectation for a
request. GET is used to fetch data, POST creates a
resource, PUT creates or replaces a resource, and
DELETE removes a resource. Following these
conventions will make your API easier to consume.

- Each HTTP response includes a status code.
Common codes include 200 OK, 201 Created,
400 Bad Request, and 404 Not Found. It’s
important to use the correct status code, as clients
use these status codes to infer the behavior of
your API.
- An HTTP API exposes methods or endpoints that
you can use to access or change data on a server
using the HTTP protocol. An HTTP API is typically
called by mobile or client-side web applications.
- You define minimal API endpoints by calling Map*
functions on the WebApplication instance,
passing in a route pattern to match and a handler
function. The handler functions runs in response to
matching requests.
- There are different extension methods for each
HTTP verb. MapGet handles GET requests, for
example, and MapPost maps POST requests. You
use these extension methods to define how your
app handles a given route and HTTP verb.
- You can define your endpoint handlers as lambda
expressions, Func<T, TResult> and
Action<T> variables, local functions, instance
methods, or static methods. The best approach
depends on how complex your handler is, as well
as personal preference.
- Returning void from your endpoint handler
generates a 200 response with no body by default.
Returning a string generates a text/plain
response. Returning an IResult instance can
generate any response. Any other object returned
from your endpoint handler is serialized to JSON.
This convention helps keep your endpoint handlers
succinct.
- You can customize the response by injecting an
HttpResponse object into your endpoint handler
and then setting the status code and response
body. This approach can be useful if you have
complex requirements for an endpoint.
- The Results and TypedResults helpers contain
static methods for generating common responses,
such as a 404 Not Found response using
Results.NotFound(). These helpers simplifying
returning common status codes.
- You can return a standard Problem Details object
by using Results.Problem() and
Results.ValiationProblem(). Problem()
generates a 500 response by default (which can
be changed), and ValidationProblem()
generates a 400 response, with a list of validation
errors. These methods make returning Problem
Details objects more concise than generating the
response manually.
- You can use helper methods to generate other
common result types on Results, such as
File() for returning a file from disk, Bytes()
for returning arbitrary binary data, and Stream()
for returning an arbitrary stream.
- You can extract common or tangential code from
your endpoint handlers by using endpoint filters,
which can keep your endpoint handlers easy to
read.
- Add a filter to an endpoint by calling
AddEndpointFilter() and providing the
lambda function to run (or use a static/instance
method). You can also implement
IEndpointFilter and call
AddEndpointFilter<T>(), where T is the
name of your implementing class.
- You can generalize your filter functions by creating
a factory, using the overload of
AddEndpointFilter() that takes an
EndpointFilterFactoryContext. You can
use this approach to support endpoint handlers
with various method signatures.
- You can reduce duplication in your endpoint routes
and filter configuration by using route groups. Call
MapGroup() on WebApplication, and provide a
prefix. All endpoints created on the returned
RouteGroupBuilder will use the prefix in their
route templates.
- You can also call AddEndpointFilter() on
route groups. Any endpoints defined on the group
will also have the filter, as though you defined
them on the endpoint directly, removing the need
to duplicate the call on each endpoint.
