using UI;
using Utils = Common.Utils;

namespace Service.Network
{
    public class NetworkPlayer : IService
    {
        private MainMenu _mainMenu;
        private Player.Player _player;
        private NetworkService _networkService;

        public Player.Player Player { set => _player = value; }

        public MainMenu MainMenu { get => _mainMenu; set => _mainMenu ??= value; }

        public void HostGame()
        {
            string matchId = Utils.GetRandomId();
            _player.CommandHostGame(matchId);
        }
        
        public void JoinGame(string matchId) => _player.CommandJoinGame(matchId);
        public void StartGame() => _player.CommandStartGame();
    }
}