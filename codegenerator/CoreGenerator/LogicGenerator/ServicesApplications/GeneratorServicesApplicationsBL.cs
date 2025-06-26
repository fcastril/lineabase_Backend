using CoreGenerator.Objects;

namespace CoreGenerator.LogicGenerator;

public class GeneratorServicesApplicationsBL : GeneratorCommonBL, IGeneratorServicesApplicationsBL
{
    #region ServicesApplications
    //Base
    private string BaseServiceApplication(string nameBase, string entity, string Dto, string interfaceService)
    {


        return $@"{nameBase}<{entity},{Dto}>, {interfaceService}";
    }
    private string BaseIServiceApplication(string nameBase, string entity, string Dto)
    {
        return $@"{nameBase}<{entity},{Dto}>";
    }

    //Service
    public void Service(string domainEntity, string layer, string[] dependencyInyections, string[] baseDependencyInyections)
    {
        string className = domainEntity + "Service";
        //TODO : Generate herency, Aplica solo para servicios de aplicacion
        string herency = BaseServiceApplication("BaseServiceApplication", domainEntity, $"{domainEntity}Dto", $"I{domainEntity}Service");
        string usings = Using([
            "Domain.Entities;",
            "Domain.Port;",
            "ServiceApplication.Base;",
            "ServiceApplication.Dto;",
            "ServiceApplication.Mapper;",

        ], false);
        string classContent = GeneratefileContent(FileTypeEnumeration.@class, className, domainEntity, layer, dependencyInyections, baseDependencyInyections, herency, usings, string.Empty, string.Empty, string.Empty, true);
        string path = $"{layer}/Models/{domainEntity}/Service";
        GenerateFileLocal(className, classContent, path);
    }

    //Interface
    public void Interface(string domainEntity, string layer)
    {
        string interfaceName = "I" + domainEntity + "Service";
        string iherency = BaseIServiceApplication("IBaseServiceApplication", domainEntity, $"{domainEntity}Dto");

        string[] usings = { "Domain.Entities;", "ServiceApplication.Dto;" };

        string InterfaceContent = GeneratefileContent(FileTypeEnumeration.@interface, interfaceName, domainEntity, layer, null, null, iherency, Using(usings, true), string.Empty);
        string path = $"{layer}/Models/{domainEntity}/Interface";
        GenerateFileLocal(interfaceName, InterfaceContent, path);
    }

    //Dto
    public void Dto(Entity entity, string layer, Dictionary<string, string> property)
    {
        string usings = Using([], true);
        string nameClass = $"{entity.Name}Dto";
        Dictionary<string, string> propertiesCsharp = new Dictionary<string, string>();
        //TODO: Cambiarlo por manejo del ENTITY de Domain
        if (entity.Properties.Count > 0)
        {
            propertiesCsharp = entity.Properties;
        }
        else
        {
            propertiesCsharp = MapSqlToCSharpTypes(property);

        }

        List<string> propertyValue = PropertyBuild(propertiesCsharp);
        string propertyFinal = Property(propertyValue.ToArray());
        string DtoContent = GeneratefileContent(FileTypeEnumeration.@class, nameClass, entity.Name, $"{layer}.Dto", null, null, "BaseDto", usings, string.Empty, propertyFinal);
        string path = $"{layer}/Models/{entity.Name}/Dto";
        GenerateFileLocal(nameClass, DtoContent, path);
    }

    //Mapper
    public void Mapper(Entity entity, string layer)
    {
        string usings = Using([
            "AutoMapper;",
            "Domain.Entities;",
            "ServiceApplication.Dto;"], false);
        string nameClass = $"{entity.Name}Mapper";

          

        string mapperEntity = $"cnf.CreateMap<{entity.Name}Dto, {entity.Name}>()";

        string properties =  string.Empty;

        foreach (var item in entity.Properties)
        {
            properties += properties.Length > 0 ? ", ": "";
            properties += $"src.{item.Key}";
        }
        
        mapperEntity += $"\n\t\t\t\t.ConstructUsing(src => src != null ? new {entity.Name}({properties}): null);";

        mapperEntity += $"\n\t\t\tcnf.CreateMap<{entity.Name}, {entity.Name}Dto>()";

        string propertiesReverMap = string.Empty;
        foreach (var item in entity.Properties)
        {
            propertiesReverMap += $"\n\t\t\t\t.ForMember(dest => dest.{item.Key}, opt => opt.MapFrom(src => src.{item.Key}))";
        }

        mapperEntity += $"{propertiesReverMap};";

        string method = Method("public static void Expresion",
            [mapperEntity],
            ["IMapperConfigurationExpression cnf"]);

        string mapperContent = GeneratefileContent(FileTypeEnumeration.@classStatic, nameClass, entity.Name, $"{layer}.Mapper", null,  null, "BaseDto", usings, method);
        string path = $"{layer}/Models/{entity.Name}/Mapper";
        GenerateFileLocal(nameClass, mapperContent, path);
    }

    //Validator
    public void Validator(string entity, string layer)
    {
        string nameClass = $"{entity}Validator";
        string usings = Using([
            "Domain.Port;",
                    "FluentValidation;",
                    "ServiceApplication.Dto;"],false);
        string[] dependencyInyectios = { $"I{entity}Repository {entity.ToLower()}Repository" };
        string property = Property([$"\t\tprivate readonly I{entity}Repository _{entity.ToLower()}Repository;"]);
        string setProperty = Property([$"_{entity.ToLower()}Repository = {entity.ToLower()}Repository;"]);
        string validatorContent = GeneratefileContent(FileTypeEnumeration.@class, nameClass, entity, "ServiceApplication.Validator", dependencyInyectios, null, $"AbstractValidator<{entity}Dto>", usings, string.Empty, property, setProperty);
        string path = $"{layer}/Models/{entity}/Validator";
        GenerateFileLocal(nameClass, validatorContent, path);
    }


    #endregion
}
