using CoreGenerator.Objects;

namespace CoreGenerator.LogicGenerator.Domain
{
    public class GeneratorDomainBL : GeneratorCommonBL, IGeneratorDomainBL
    {
        public void Context(Entity entity, string layer)
        {

            string herency = BaseDomain(entity.Name);
            string[] usings = ["Domain.Common;"];
            string className = entity.Name;
            string classContent = GeneratefileContentDomain(
                    entity, usings, herency);

            layer = $"{layer}/Context/{entity.Name}";
            GenerateFileLocal(className, classContent, layer);
        }

        private string BaseDomain(string nameEntity)
        {
            return $@"{nameEntity}: BaseEntity";
        }


        public string GeneratefileContentDomain(Entity entity, string[] usings, string herency)
        {
            string ctor = $@"{ConstructorDomain(entity.Name, entity.Properties)}";
            string ctorBasic =  $@"public {entity.Name}() {{ }}";

            string properties = string.Join('\n', 
                entity.Properties.
                    Select(p => $"\t\tpublic {p.Value} {p.Key} {{ get; private set; }}"));

            string contentClass = 
$@"public class {herency}
    {{
        {ctorBasic}
        {ctor}
{properties}
    }}";

return
$@"{Using(usings, false)}
namespace Domain.Entities
{{
    {contentClass}
}}";
        }

        private string ConstructorDomain(string name, Dictionary<string, string> properties)
        {
            return 
$@"public {name}({string.Join(", ", properties.Select(p => $"{p.Value} {p.Key.ToLower()}"))})
        {{
{   string.Join("\r", properties.Select(p => $"\t\t\t{p.Key} = {p.Key.ToLower()};"))}
        }}";
        }
    }
}