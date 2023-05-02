using Common;

namespace Service.Network
{
    public class NetworkService : IService
    {
        private readonly CustomNetworkManager _networkManager;

        public MatchMaker MatchMaker { get; }

        public NetworkService(CustomNetworkManager networkManager, MatchMaker matchMaker)
        {
            _networkManager = networkManager;
            MatchMaker = matchMaker;
        }

        public void StartClient() => _networkManager.StartClient();
        public void StartHost() => _networkManager.StartHost();

        public void StartServer() => _networkManager.StartServer();
        
        public void StartGame(string matchId) => MatchMaker.StartGame(matchId);
    }
}