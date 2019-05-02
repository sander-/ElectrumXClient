using ElectrumXClient;
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

        [Test]
        public async Task Test_CanGetVersion()
        {
            var response = await _client.GetVersion();
            _client.Dispose();
        }
    }
}
