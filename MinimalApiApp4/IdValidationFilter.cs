namespace MinimalApiApp4
{
    /*
     * You can implement IEndpointFilter by defining a class with an InvokeAsync() that has the same signature as the lambda
     * The advantage of using IEndpointFilter is that you get IntelliSense and autocompletion for the method signature.
     */
    public class IdValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<string>(0);
            if (string.IsNullOrEmpty(id))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"id", new[] {"The id parameter is required"} }
                });
            }
            return await next(context);
        }
    }
}
