namespace CoreGenerator.LogicGenerator;

public class GeneratorInfraestructureBL : GeneratorCommonBL , IGeneratorInfraestructureBL
{
    #region  Infraestructure
    
            //Infraestructure
            public void Repository(string baseRepository,string domainEntity,string className,string[] dependencyInyections,string[] baseDependencyInyections,string[] layers){

                string herency = BaseRepository(baseRepository, "IRepositoryBase",domainEntity,$"I{className}");
                string usings = Using([
                    "Domain.Entities;",
                    "Domain.Port;"

                ], false);
                string path = $"{layers[3]}/Repository";
                string classContent = GeneratefileContent(FileTypeEnumeration.@class, className,domainEntity, $"{layers[3]}.Repository", dependencyInyections, baseDependencyInyections, herency,usings,string.Empty);
                GenerateFileLocal(className, classContent,path);

            }

            public void IRepository(string domainEntity,string className,string[] layers){
                string usings = Using(["Domain.Entities;"], false);
                string interfaceName = "I" + className;
                string iherency = IBaseRepository(domainEntity);
                string InterfaceContent = GeneratefileContent(FileTypeEnumeration.@interface, interfaceName,domainEntity, $"{layers[2]}.Port", null, null, iherency,usings,string.Empty);
                GenerateFileLocal(interfaceName, InterfaceContent,$"{layers[2]}/Port");
            }

            private string BaseRepository(string nameBase,string interfaceBase,string entity,string interfaceRepository){
                return $@"{nameBase}<{entity}>, {interfaceRepository}";
            }

            private string IBaseRepository(string entity){
                return $@"IRepositoryBase<{entity}>";
            }
        #endregion
}
