using Moq;
using Api.Installers;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Api.Common.Installers {
  [TestClass]
  public class RequiredHeaderParameterTest {

    [TestMethod]
    public void ApplyTest() {
      RequiredHeaderParameter requiredHeaderParameter = new RequiredHeaderParameter();
      OpenApiOperation operation = new OpenApiOperation();
      Mock<ApiDescription> apiDescription = new Mock<ApiDescription>();
      Mock<ISchemaGenerator> schemaGenerator = new Mock<ISchemaGenerator>();
      SchemaRepository schemaRepository = new SchemaRepository();
      Mock<MethodInfo> methodInfo = new Mock<MethodInfo>();

      OperationFilterContext context = new OperationFilterContext(apiDescription.Object, schemaGenerator.Object, schemaRepository, methodInfo.Object);

      requiredHeaderParameter.Apply(operation, context);
    }
  }
}
