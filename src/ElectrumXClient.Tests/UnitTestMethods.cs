using ElectrumXClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

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
        public void Test_CanGetVersion()
        {
            var response = _client.GetVersion();
            _client.Dispose();
        }
    }
}
