using ElectrumXClient.Response;
using System;
using System.Threading.Tasks;

namespace ElectrumXClient
{
    public interface IClient : IDisposable
    {
        Task<BlockchainTransactionBroadcastResponse> BlockchainTransactionBroadcast(string tx);
        Task<BlockchainBlockHeaderResponse> GetBlockchainBlockHeader();
        Task<BlockchainEstimatefeeResponse> GetBlockchainEstimatefee(uint number);
        Task<BlockchainScripthashListunspentResponse> GetBlockchainListunspent(string scripthash);
        Task<BlockchainNumblocksSubscribeResponse> GetBlockchainNumblocksSubscribe();
        Task<BlockchainScripthashGetBalanceResponse> GetBlockchainScripthashGetBalance(string scripthash);
        Task<BlockchainScripthashGetHistoryResponse> GetBlockchainScripthashGetHistory(string scripthash);
        Task<BlockchainTransactionGetResponse> GetBlockchainTransactionGet(string txhash);
        Task<ServerFeaturesResponse> GetServerFeatures();
        Task<ServerPeersSubscribeResponse> GetServerPeersSubscribe();
        Task<ServerVersionResponse> GetServerVersion();
    }
}