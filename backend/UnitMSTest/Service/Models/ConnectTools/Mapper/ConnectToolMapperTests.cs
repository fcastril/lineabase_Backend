using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ConnectToolMapperTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            var config = new MapperConfiguration(cnf =>
            {
                ConnectToolMapper.Expresion(cnf);
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Map_ConnectToolDto_To_ConnectTool_Should_Work_Correctly()
        {
            var connectToolDto = new ConnectToolDto
            {
                DiscoveryId = "TestDiscoveryId",
                Organization = "TestOrg",
                PAT = "TestPAT"
            };

            var connectTool = _mapper.Map<ConnectTool>(connectToolDto);

            Assert.IsNotNull(connectTool);
            Assert.AreEqual(connectToolDto.DiscoveryId, connectTool.DiscoveryId);
            Assert.AreEqual(connectToolDto.Organization, connectTool.Organization);
            Assert.AreEqual(connectToolDto.PAT, connectTool.PAT);
        }

        [TestMethod]
        public void Map_Null_ConnectToolDto_To_ConnectTool_Should_Return_Null()
        {
            ConnectToolDto connectToolDto = null;

            var connectTool = _mapper.Map<ConnectTool>(connectToolDto);

            Assert.IsNull(connectTool);
        }
    }
}