using CoreGenerator.Objects;

namespace CoreGenerator
{
    public interface ICoreGenerator
    {
        Task GodaVinci(DaVinciDto daVinciDto);
        Task GenerateDomain(Entity entity);
        Task GenerateServicesApplication(Entity entity);
        Task GenerateInfraestructure(Entity entity);
        Task GenerateApi(Entity entity);
    }
}
