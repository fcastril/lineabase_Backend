namespace CoreGenerator.LogicGenerator;

public interface IGeneratorInfraestructureBL
{
   void Repository(string baseRepository,string domainEntity,string className,string[] dependencyInyections,string[] baseDependencyInyections,string[] layers);
   void IRepository(string domainEntity,string className,string[] layers);
}
