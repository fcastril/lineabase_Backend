using CoreGenerator.Objects;

namespace CoreGenerator.LogicGenerator;

public class GeneratorCommonBL
{
    #region Transversal
    //Transversal
    public string Constructor(string[]? baseDependecyInyections,
        string[]? dependecyInyections,
        string setProperty,
        string className,
        string entity,
        bool includeMapper)
    {

        string ctorBase = string.Empty;
        string ctorParam = string.Empty;
        string ctorMapper = string.Empty;
        if (baseDependecyInyections is not null)
            ctorBase = $@": base({string.Join(',', baseDependecyInyections)})";

        if (dependecyInyections is not null)
            ctorParam = string.Join(',', dependecyInyections);

        if (includeMapper)
        {
            ctorMapper = $@"
            CreateMapperExpresion<{entity}, {entity}Dto>(cnf =>
            {{
                {entity}Mapper.Expresion(cnf);
            }});";
        }

        return 
$@"public {className}({ctorParam}){ctorBase}
        {{
            {setProperty}
            {ctorMapper}
        }}";
    }

    public string GeneratefileContent(FileTypeEnumeration type, string name, string entity, string nameSpace,
                                        string[]? dependecyInyections, string[]? baseDependecyInyections,
                                         string herency, string usings, string method, string propertry = "", string setProperty = "",
                                         bool isService = false)
    {
        string ctor = string.Empty;
        if (type is FileTypeEnumeration.@class || type is FileTypeEnumeration.@controller)
        {
            ctor = $@"{Constructor(baseDependecyInyections, dependecyInyections, setProperty, name, entity, isService)}";
        }

        if (type is FileTypeEnumeration.@classStatic)
        {
            return 
$@"{usings}

namespace {nameSpace}
{{
    public static class {name}
    {{
        {propertry}
        {method}
    }}
}}";
        }
        else if (type is FileTypeEnumeration.@controller)
        {
            return 
$@"{usings}
namespace {nameSpace}
{{
    [Route(Constants.UriForDefaultWebApi+""[controller]"")]
    [ApiController]
    public {FileTypeEnumeration.@class} {name} : {herency}
    {{
        {propertry}
        {ctor}
        {method}
    }}
}}";
        }

        return 
$@"{usings}

namespace {nameSpace}
{{
    public {type} {name} : {herency}
    {{
{propertry}
        {ctor}
        {method}
    }}
}}";
    }


    public void GenerateFileLocal(string className, string classContent, string path)
    {
        string currentDirectory = AppContext.BaseDirectory;
        string? projectRoot = Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        string realPath = $"{projectRoot}/FileOutput/{path}";
        string filePath = Path.Combine(realPath, $"{className}.cs");

        if (!Directory.Exists(realPath) && projectRoot is not null)
        {
            Console.WriteLine("La ruta especificada no existe. se creará");
            Directory.CreateDirectory(realPath);
        }
        File.WriteAllText(filePath, classContent);
    }

    public string Method(string signature, string[] content, string[]? parameters)
    {
        string parametersMethod = "";
        string contentMethod = $@"throw new NotImplementedException();";

        if (parameters is not null)
            parametersMethod = string.Join(',', parameters);
        if (content is not null)
        {
            contentMethod = string.Empty;
            foreach (var item in content)
            {
                contentMethod += $@"{item}";
            }
        }
        return 
            $@"{signature} ({parametersMethod})
        {{
            {contentMethod}
        }}";
    }

    public string Using(string[] lst, bool usingBasic = true)
    {
        string contentUsing = string.Empty;

        if (usingBasic)
            contentUsing = $@"using System;";


        foreach (var item in lst)
        {
            contentUsing += $"\nusing {item}";
        }
        return contentUsing;
    }

    public string Property(string[] lst)
    {
        string contentProperty = "";
        foreach (var item in lst)
        {
            contentProperty += $"{item}";
        }
        return contentProperty;
    }

    public List<string> PropertyBuild(Dictionary<string, string> values)
    {
        var lstResult = new List<string>();

        foreach (var item in values)
        {
            lstResult.Add($"\t\tpublic {item.Value} {item.Key} {{ get; set; }}\n");
        }
        return lstResult;
    }

    public Dictionary<string, string> MapSqlToCSharpTypes(Dictionary<string, string> sqlProperties)
    {
        var csharpProperties = new Dictionary<string, string>();

        foreach (var prop in sqlProperties)
        {
            string propertyName = prop.Key;
            string sqlType = prop.Value.ToLower(); // Convertir el tipo de SQL a minúsculas para manejar los tipos correctamente
            string csharpType;

            switch (sqlType)
            {
                case "int":
                    csharpType = "int";
                    break;
                case "bigint":
                    csharpType = "long";
                    break;
                case "bit":
                    csharpType = "bool";
                    break;
                case "char":
                case "varchar":
                case "nvarchar":
                case "text":
                    csharpType = "string";
                    break;
                case "decimal":
                case "money":
                case "smallmoney":
                    csharpType = "decimal";
                    break;
                case "float":
                    csharpType = "double";
                    break;
                case "real":
                    csharpType = "float";
                    break;
                case "datetime":
                case "smalldatetime":
                case "date":
                case "time":
                case "datetime2":
                    csharpType = "DateTime";
                    break;
                case "uniqueidentifier":
                    csharpType = "Guid";
                    break;
                case "binary":
                case "varbinary":
                case "image":
                    csharpType = "byte[]";
                    break;
                case "tinyint":
                    csharpType = "byte";
                    break;
                case "smallint":
                    csharpType = "short";
                    break;
                default:
                    csharpType = "object"; // Si el tipo no es reconocido, se asigna como object
                    break;
            }

            // Agregar el nombre de la propiedad y su tipo en C# al diccionario
            csharpProperties.Add(propertyName, csharpType);
        }

        return csharpProperties;
    }



    #endregion
}
