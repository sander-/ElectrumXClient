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
        public async Task Test_CanGetServerFeatures()
        {
            var response = await _client.GetServerFeatures();
            Assert.IsInstanceOf<ServerFeaturesResponse>(response);
        }

        [Test]
        public async Task Test_CanGetServerPeersSubscribe()
        {
            var response = await _client.GetServerPeersSubscribe();
            Assert.IsInstanceOf<ServerPeersSubscribeResponse>(response);
        }

        [Test]
        public async Task Test_CanGetBlockchainNumblocksSubscribe()
        {
            var response = await _client.GetBlockchainNumblocksSubscribe();
            Assert.IsInstanceOf<BlockchainNumblocksSubscribeResponse>(response);
        }

        [Test]
        public async Task Test_CanGetBlockchainBlockHeader()
        {
            var response = await _client.GetBlockchainBlockHeader();
            Assert.IsInstanceOf<BlockchainBlockHeaderResponse>(response);
        }

        [Test]
        public async Task Test_CanGetBlockchainScripthashGetBalance()
        {
            var response = await _client.GetBlockchainScripthashGetBalance("000008876cc4a4550d368ec40f7a1e8a17b665f422be9c53266b51ca3ab8b1d1");
            Assert.IsInstanceOf<BlockchainScripthashGetBalanceResponse>(response);
        }

        [Test]
        public async Task Test_CanGetBlockchainTransactionGet()
        {
            var response = await _client.GetBlockchainTransactionGet("901d8af29d41cd7a5e01f0f5d5423d435ff88c180fb6f2f9e4a90fdb6da62e54");
            Assert.IsInstanceOf<BlockchainTransactionGetResponse>(response);
        }

    }
}
