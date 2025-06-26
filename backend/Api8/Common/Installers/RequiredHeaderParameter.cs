using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Installers {
  public class RequiredHeaderParameter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
      operation.Parameters ??= new List<OpenApiParameter>();

      operation.Parameters.Add(new OpenApiParameter {
        Name = "x-header-custom",
        In = ParameterLocation.Header,
        Required = false,
        Example = new OpenApiString("c65c51d3-46cb-429e-bbed-dab82d565b12")
      });
    }
  }
}
