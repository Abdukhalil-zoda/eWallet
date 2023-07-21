using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eWallet
{
    public class SwaggerOperationFilters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Add your custom header here
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Digest",
                In = ParameterLocation.Header,
                Description = "Your custom header description",
                Required = false, 
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-UserId",
                In = ParameterLocation.Header,
                Description = "Your custom header description",
                Required = false, 
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }

    }
}
