using System.Reflection;

namespace MinimalApiApp3
{
    public class ValidationHelper
    {
        internal static EndpointFilterDelegate ValidateIdFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
        {
            ParameterInfo[] parameters = context.MethodInfo.GetParameters();
            Console.WriteLine(parameters);
            int? idPosition = null;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Name == "id" && parameters[i].ParameterType == typeof(string))
                {
                    idPosition = i;
                    break;
                }
            }

            if (!idPosition.HasValue)
            {
                return next;
            }

            return async (invocationContext) =>
            {
                var id = invocationContext.GetArgument<string>(idPosition.Value);
                if (string.IsNullOrEmpty(id))
                {
                    return Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        {"id", new[] {"The id parameter is required"} }
                    });
                }
                return await next(invocationContext);
            };
        }
    }
}

/*
❷The context parameter provides details about the endpoint handler method.
❸ GetParameters() provides details about the parameters of the handler being called.
❹ Loops through the parameters to find the string id parameter and record its position
❺ If the id parameter isn’t not found, doesn’t add a filter, but returns the remainder of the
pipeline
❻ If the id parameter exists, returns a filter function (the filter executed for the endpoint)
❼ If the id isn’t valid, returns a Problem Details result
❽ If the id is valid, executes the next filter in the pipeline
*/

