using CoreGenerator.Objects;

namespace CoreGenerator.LogicGenerator;

public interface IGeneratorServicesApplicationsBL
{
    void Service(string domainEntity,string layer,string[] dependencyInyections,string[] baseDependencyInyections);
    void Interface(string domainEntity,string layer);
    void Dto(Entity entity,string layer,Dictionary<string,string> property);
    void Mapper(Entity entity,string layer);
    void Validator(string entity,string layer);

}
