using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CoreGenerator.LogicGenerator;
using CoreGenerator.LogicGenerator.Domain;
using CoreGenerator.LogicGenerator.UnitMSTest;
using CoreGenerator.LogicSqlServer;
using CoreGenerator.Objects;
using SqlServerConnector.SqlContext;

namespace CoreGenerator
{
    public class GeneratorBL : ICoreGenerator
    {

        private readonly IGeneratorInfraestructureBL _generatorInfraestructureBL;
        private readonly IGeneratorServicesApplicationsBL _generatorServicesApplicationsBL;
        private readonly IGeneratorApi _generatorApi;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IArchitectureBL _architectureBL;
        private readonly ISqlServerBL _sqlServerBL;
        private readonly IGeneratorDomainBL _generatorDomainBL;
        private readonly IGenerateUnitMSTEstBL _generateUnitMSTEstBL;
        private string[] layers = {"Api",
                    "ServiceApplication",
                    "Domain",
                    "Infrastructure",
                    "UnitMSTest"};

        public GeneratorBL(IGeneratorInfraestructureBL generatorInfraestructureBL,
                            IGeneratorServicesApplicationsBL generatorServicesApplicationsBL,
                            IGeneratorApi generatorApi,
                            IApplicationDbContext applicationDbContext,
                            IArchitectureBL architectureBL,
                            ISqlServerBL sqlServerBL,
                            IGeneratorDomainBL generatorDomainBL,
                            IGenerateUnitMSTEstBL generateUnitMSTEstBL)
        {
            _generatorInfraestructureBL = generatorInfraestructureBL;
            _generatorServicesApplicationsBL = generatorServicesApplicationsBL;
            _generatorApi = generatorApi;
            _applicationDbContext = applicationDbContext;
            _architectureBL = architectureBL;
            _sqlServerBL = sqlServerBL;
            _generatorDomainBL = generatorDomainBL;
            _generateUnitMSTEstBL = generateUnitMSTEstBL;
        }

        public async Task GodaVinci(DaVinciDto daVinciDto)
        {
            // var architecture = await _architectureBL.FirstOrDefault(f => f.Code == daVinciDto.CodeArchitecture);

            // if (architecture == null)
            // {
            //     throw new Exception("The architecture selected is not found");
            // }
            //layers = ["Api8", "Domain", "Infraestructure", "ServiceApplication", "Unit"];

            foreach (var entity in daVinciDto.Entities)
            {
                await GenerateDomain(entity);
                await GenerateApi(entity);
                await GenerateServicesApplication(entity);
                await GenerateInfraestructure(entity);
                await GenerateUnitMSTest(entity);

            }
        }

        private async Task GenerateUnitMSTest(Entity entity)
        {
            _generateUnitMSTEstBL.GenerateApiTest(entity, layers[4]);
            await Task.CompletedTask;
        }

        public async Task GenerateDomain(Entity entity)
        {
            _generatorDomainBL.Context(entity, layers[2]);
            await Task.CompletedTask;
        }
        public async Task GenerateServicesApplication(Entity entity)
        {
            string domainEntity = entity.Name;
            //SERVICE APPLICATIONS
            string[] dependencyInyections = { $"I{domainEntity}Repository {domainEntity.ToLower()}Repository"};
            string[] baseDependencyInyections = { $"{domainEntity.ToLower()}Repository"};

            //Services
            _generatorServicesApplicationsBL.Service(domainEntity, layers[1], dependencyInyections, baseDependencyInyections);

            //Interface
            _generatorServicesApplicationsBL.Interface(domainEntity, layers[1]);

            //Dto
            //TODO : Estrategia de carga de propiedades de entidad de la DB
            Dictionary<string, string> property = entity.Properties; //await _sqlServerBL.GetTableProperties(codeProject, domainEntity);
            _generatorServicesApplicationsBL.Dto(entity, layers[1], property);

            //Mapper
            _generatorServicesApplicationsBL.Mapper(entity, layers[1]);

            //Validator
            _generatorServicesApplicationsBL.Validator(domainEntity, layers[1]);

            await Task.CompletedTask;
        }

        public async Task GenerateInfraestructure(Entity entity)
        {
            string domainEntity = entity.Name;
            //INFRAESTRUCTURE
            string contextDb = "MainContextCosmos"; //TODO : Estrategia de contexto de db relacional o no relacional
            string baseRepository = "RepositoryBase"; //TODO : Estrategia de base de db relacional o no relacional

            string[] dependencyInyections = { $"I{contextDb} mainContext" };
            string[] baseDependencyInyections = { "mainContext" };

            string className = domainEntity + "Repository";

            //TODO : Generate herency, Aplica solo para repository
            //Repository
            _generatorInfraestructureBL.Repository(baseRepository, domainEntity, className, dependencyInyections, baseDependencyInyections, layers);

            //Interface Repository
            _generatorInfraestructureBL.IRepository(domainEntity, className, layers);

            await Task.CompletedTask;
        }

        public async Task GenerateApi(Entity entity)
        {
            string domainEntity = entity.Name;
            //Controller
            _generatorApi.Controller(domainEntity, layers[0]);

            //DependencyInyecctions
            // TODO : Generar inyecciones de dependencia para el programs.
            await Task.CompletedTask;
        }


    }
}
