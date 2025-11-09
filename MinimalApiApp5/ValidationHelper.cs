namespace MinimalApiApp5
{
    public class ValidationHelper
    {
        internal static async ValueTask<object?> ValidateIdFactory(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<string>(0);

            if (string.IsNullOrEmpty(id))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"id", new[] {"Invalid id"} }
                });
            }
            return await next(context);
        }
    }
}
