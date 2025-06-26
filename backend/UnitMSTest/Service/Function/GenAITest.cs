using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using ServiceApplication.Functions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class GenAITest
    {
        Mock<HttpClient> httpClientMock;
        Mock<IConfiguration> configurationMock;
        IGenAI genAI;

        [TestInitialize]
        public void Init()
        {
            httpClientMock = new Mock<HttpClient>();
            configurationMock = new Mock<IConfiguration>();
            genAI = new GenAI(httpClientMock.Object, configurationMock.Object);
        }

        [TestMethod]
        public async Task SendAsyncSuccessfulTest()
        {
            string discoveryId = "12345";
            string name = "test";
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("true"),
                });

            var httpClient = new HttpClient(handlerMock.Object);
            genAI = new GenAI(httpClient, configurationMock.Object);

            configurationMock.Setup(x => x.GetSection("Functions:GenAI:UrlBase").Value).Returns("https://localhost:1000");
            configurationMock.Setup(x => x.GetSection("Functions:GenAI:OrchMethod").Value).Returns("/api/GenOrch");

            var result = await genAI.SendAsync(discoveryId, name);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
