using CoreGenerator.Objects;

namespace CoreGenerator.LogicGenerator.UnitMSTest
{
    public class GenerateUnitMSTEstBL : GeneratorCommonBL, IGenerateUnitMSTEstBL
    {
        public void GenerateApiTest(Entity entity, string layer)
        {
            string herency  = BaseTest(entity.Name);
            string[] usings = [
                "Api.Controllers;",
                "Domain.Entities;",
                "ServiceApplication.Dto;",
                "UnitMSTest.Api.Controllers.Base;",
                "Microsoft.VisualStudio.TestTools.UnitTesting;"];
            string className = $"{entity.Name}ControllerTest";
            string classContent = GeneratefileContentTest(entity, usings, herency);

            string path = $"{layer}/Api/Controllers";
            GenerateFileLocal(className, classContent, path);
        }

        private string GeneratefileContentTest(Entity entity, string[] usings, string herency)
        {
            return 
$@"{Using(usings, false)}

namespace UnitMSTest.Api.Controllers
{{
    [TestClass]
    public class {herency}
    {{
    }}
}}";
        }

        private string BaseTest(string name)
        {
            return $@"{name}ControllerTest : HandlerBaseControllerTest<{name}, {name}Dto, {name}Controller>";
        }
    }
}