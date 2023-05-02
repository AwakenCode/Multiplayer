using System;
using Common;
using Mirror;
using Service;
using Service.Network;
using UnityEngine;
using NetworkPlayer = Service.Network.NetworkPlayer;

namespace Player
{
    [RequireComponent(typeof(NetworkMatch))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private Movement _movement;
        [SerializeField] private ColorChanger _colorChanger;

        [SyncVar] private string _matchId;
        private NetworkMatch _networkMatch;
        private SceneLoader _sceneLoader;
        private NetworkService _networkService;
        private NetworkPlayer _networkPlayer;

        private void Awake()
        {
            _networkMatch = GetComponent<NetworkMatch>();
            _sceneLoader = Services.Container.Resolve<SceneLoader>();
            _networkService = Services.Container.Resolve<NetworkService>();
            _networkPlayer = Services.Container.Resolve<NetworkPlayer>();
        }

        private void Start()
        {
            if (isLocalPlayer == false) 
                _networkPlayer.MainMenu.LobbyWindow.SpawnPlayerLabel();
        }

        public override void OnStartClient()
        {
            if (isLocalPlayer == false) return;

            _networkPlayer.Player = this;
            DontDestroyOnLoad(gameObject);
        }

        [Command]
        public void CommandHostGame(string matchId)
        {
            _networkService.MatchMaker.CreateMatch(matchId, this, out bool isSuccess);
            TargetInitLobby(matchId, isSuccess, true);
            EnterMatch(matchId, isSuccess);
        }

        [Command]
        public void CommandJoinGame(string matchId)
        {
            _networkService.MatchMaker.JoinMatch(matchId, this, out bool isSuccess);
            TargetInitLobby(matchId, isSuccess, false);
            EnterMatch(matchId, isSuccess);
        }

        [Command]
        public void CommandStartGame() => _networkService.StartGame(_matchId);

        [TargetRpc]
        public void TargetLoadGame()
        {
            _sceneLoader.Load(Constants.GameSceneName, true, () =>
            {
                _networkPlayer.MainMenu.CloseMainMenu();
                SetActive();
            });
        }

        [TargetRpc]
        private void TargetInitLobby(string matchId, bool isSuccess, bool isHost)
        {
            if(isSuccess == false) return;
            _networkPlayer.MainMenu.ShowLobby(matchId, isHost);
            _networkPlayer.MainMenu.LobbyWindow.InitPlayerLabel();
        }

        private void EnterMatch(string matchId, bool isSuccess)
        {
            if (isSuccess == false) return; 
            
            _matchId = matchId;
            _networkMatch.matchId = _matchId.ToGuid();
        }

        private void SetActive()
        {
            _movement.SetActiveGravity(true);
            _colorChanger.SetActive(true);
        }
    }
}