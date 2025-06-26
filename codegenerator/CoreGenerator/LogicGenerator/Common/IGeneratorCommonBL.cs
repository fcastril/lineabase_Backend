namespace CoreGenerator.LogicGenerator;

public interface IGeneratorCommonBL
{
    string Constructor(string[]? baseDependecyInyections, string[]? dependecyInyections,string setProperty, string className,string entity,bool includeMapper);
    string GeneratefileContent(FileTypeEnumeration type, string name,string entity, string nameSpace, 
                                                string[]? dependecyInyections, string[]? baseDependecyInyections,
                                                 string herency,string usings,string method,string propertry="",string setProperty="",bool isService=false);
    void GenerateFileLocal(string className,string classContent,string path);

    string Method(string signature,string[] content,string[]? parameters);

    string Using(string[] lst);

    string Property(string[] lst);
}
