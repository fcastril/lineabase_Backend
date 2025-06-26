namespace CoreGenerator.LogicGenerator;

public class GeneratorApiBL : GeneratorCommonBL, IGeneratorApi
{
     #region Api
            private string BaseController(string name,string entity)
            {
                return $"{name}<{entity},{entity}Dto>";
            }

            public void Controller(string entity,string layer)
            {
                string nameController =$"{entity}Controller";
                string[] dependencyInyections = { "IMediator mediator", $"IValidator<{entity}Dto> validator"};
                string[] baseDependencyInyections = { "validator","mediator" };
                string usings=Using([
                    "Api.Base;",
                    "Domain.Entities;",
                    "FluentValidation;",
                    "MediatR;",
                    "Microsoft.AspNetCore.Mvc;",
                    "ServiceApplication.Dto;",
                    "Utilidades;"
                    ],false);
                string herency = BaseController("HandlerBaseController",entity);
                string content = GeneratefileContent(FileTypeEnumeration.@controller, nameController, entity, $"{layer}.Controllers", 
                                                                dependencyInyections, baseDependencyInyections,
                                                                herency,usings,string.Empty);

                GenerateFileLocal(nameController, content,$"{layer}8/Controllers");
            }

        #endregion
}
