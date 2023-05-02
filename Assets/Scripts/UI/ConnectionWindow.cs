using System;
using Service;
using Service.Network;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ConnectionWindow : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;

        private NetworkService _networkService;
        
        public event Action OnEnter;

        private void Start()
        {
            _networkService = Services.Container.Resolve<NetworkService>();

            // if (Application.isBatchMode == false)
            // {
            //     Debug.Log("Client build");
            //     _networkService.StartClient();
            // }
            // else
            // {
            //     Debug.Log("Server build");
            //     _networkService.StartHost();
            // }
        }

        private void OnEnable()
        {
            _hostButton.onClick.AddListener(OnHostButtonClicked);
            _clientButton.onClick.AddListener(OnClientEnter);
        }

        private void OnClientEnter()
        {
            _networkService.StartClient();
            OnEnter?.Invoke();
        }

        private void OnHostButtonClicked()
        {
            _networkService.StartHost();
            OnEnter?.Invoke();
        }
    }
}