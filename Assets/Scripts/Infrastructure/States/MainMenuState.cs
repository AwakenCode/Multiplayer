using Common;
using Service;
using Service.Factory;
using UI;
using NetworkPlayer = Service.Network.NetworkPlayer;

namespace Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly UIFactory _uiFactory;
        private readonly NetworkPlayer _networkPlayer;

        private MainMenu _mainMenu;

        public MainMenuState(SceneLoader sceneLoader, UIFactory uiFactory, NetworkPlayer networkPlayer)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _networkPlayer = networkPlayer;
        }
        
        public void Enter() => _sceneLoader.Load(Constants.MainMenuSceneName, onLoaded: OnLoaded);

        public void Exit() => _mainMenu.LobbyWindow.StartButtonClicked -= NextState;
        
        private void OnLoaded()
        {
            _mainMenu = _uiFactory.CreateMainMenu();
            _networkPlayer.MainMenu = _mainMenu;
            _mainMenu.LobbyWindow.StartButtonClicked += NextState;
        }
        
        private void NextState(string matchId) => _networkPlayer.StartGame();
    }
}