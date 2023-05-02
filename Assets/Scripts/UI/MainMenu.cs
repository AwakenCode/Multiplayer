using Mirror;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NetworkPlayer = Service.Network.NetworkPlayer;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _createButton;
        [SerializeField] private LobbyWindow _lobbyWindow;
        [SerializeField] private TMP_InputField _joinField;
        [SerializeField] private RectTransform _menuWindow;
        
        private NetworkPlayer _networkPlayer;
        
        public LobbyWindow LobbyWindow => _lobbyWindow;

        private void Awake() => _networkPlayer = Services.Container.Resolve<NetworkPlayer>();

        private void Start() => ShowMenu();

        private void OnEnable()
        {
            _joinButton.onClick.AddListener(OnJoinButtonClicked);
            _createButton.onClick.AddListener(OnCreateButtonClicked);
        }

        private void OnDisable()
        {
            _joinButton.onClick.RemoveListener(OnJoinButtonClicked);
            _createButton.onClick.RemoveListener(OnCreateButtonClicked);
        }

        public void ShowLobby(string matchId, bool isHost)
        {
            CloseMenu();
            _lobbyWindow.Show(matchId, isHost);
        }
        
        public void CloseMainMenu()
        {
            _lobbyWindow.Close();
            CloseMenu();
        }
        
        private void CloseMenu() => _menuWindow.gameObject.SetActive(false);

        private void ShowMenu()
        {
            _lobbyWindow.Close();
            _menuWindow.gameObject.SetActive(true);
        }
        
        private void OnCreateButtonClicked() => _networkPlayer.HostGame();

        private void OnJoinButtonClicked() => _networkPlayer.JoinGame(_joinField.text.ToUpper());
    }
}