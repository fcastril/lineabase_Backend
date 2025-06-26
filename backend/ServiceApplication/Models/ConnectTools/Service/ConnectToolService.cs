using Domain.Common;
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Events;
using ServiceApplication.Mapper;
using ServiceApplication.Port;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public class ConnectToolService : BaseServiceApplication<ConnectTool, ConnectToolDto>, IConnectToolService
    {
        private readonly IMessageSender<CommGeneric> _messageSender;

        public ConnectToolService(IConnectToolRepository connectToolRepository, IMessageSender<CommGeneric> messageSender) : base(connectToolRepository)
        {
            CreateMapperExpresion<ConnectTool, ConnectToolDto>(cnf =>
            {
                ConnectToolMapper.Expresion(cnf);
            });

            _messageSender = messageSender;
        }

        /// <summary>
        /// crear una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<ConnectToolDto> CreateModel(ConnectToolDto dto)
        {
            ConnectToolDto connectTool = await base.CreateModel(dto);
            await SendMessageToQueue(dto);

            return connectTool;
        }

        public override async Task<ConnectToolDto> UpdateModel(ConnectToolDto dto)
        {
            ConnectToolDto connectTool = await base.UpdateModel(dto);
            await SendMessageToQueue(dto);

            return connectTool;
        }

        private async Task SendMessageToQueue(ConnectToolDto dto)
        {
            await _messageSender.SendCommAsync(new()
            {
                DiscoveryId = dto.DiscoveryId,
                Organization = dto.Organization,
                PersonalToken = dto.PAT,
                Tool = Tools.ADO.ToString()
            });
        }
    }
}
