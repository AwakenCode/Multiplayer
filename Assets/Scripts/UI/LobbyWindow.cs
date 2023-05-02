using System;
using Service;
using Service.Factory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyWindow : MonoBehaviour
    {
        [SerializeField] private Transform _playerListContent;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _roomId;
        
        private UIFactory _uiFactory;
        private PlayerLabelUI _playerLabel;

        public event Action<string> StartButtonClicked;

        private void Awake() => _uiFactory = Services.Container.Resolve<UIFactory>();

        private void OnEnable() => _startButton.onClick.AddListener(OnStartGameButtonClicked);

        private void OnDisable() => _startButton.onClick.RemoveListener(OnStartGameButtonClicked);

        public void Show(string matchId, bool isHost)
        {
            gameObject.SetActive(true);
            _roomId.text = matchId;
            
            if (isHost) return;

            _startButton.interactable = false;
        }

        public void InitPlayerLabel()
        {
            if (_playerLabel != null) 
                Destroy(_playerLabel);
            SpawnPlayerLabel();
        }

        public void SpawnPlayerLabel()
        {
            Debug.Log(1);
            _uiFactory.CreatePlayerLabel(_playerListContent);
        }

        public void Close() => gameObject.SetActive(false);

        private void OnStartGameButtonClicked() => StartButtonClicked?.Invoke(_roomId.text);
    }
}