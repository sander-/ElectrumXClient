using ElectrumXClient;
using ElectrumXClient.Response;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ElectrumXClient.Tests
{
    public class UnitTestMethods
    {
        private Client _client;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("client-secrets.json")
                .Build();
            var host = config["ElectrumXHost"];
            var port = 50001;
            var useSSL = false;

            _client = new Client(host, port, useSSL);
        }

        [TearDown]
        public void Teardown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task Test_CanGetServerVersion()
        {
            var response = await _client.GetServerVersion();
            Assert.IsInstanceOf<ServerVersionResponse>(response);
        }

        [Test]
        public async Task Test_CanGetServerPeersSubscribe()
        {
            var response = await _client.GetServerPeersSubscribe();
            Assert.IsInstanceOf<ServerPeersSubscribeResponse>(response);
        }

    }
}
