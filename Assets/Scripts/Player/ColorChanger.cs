using System.Collections;
using Data;
using Mirror;
using Service;
using Service.Data;
using UnityEngine;

namespace Player
{
    public class ColorChanger : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private PlayerSnatch _playerSnatch;
        
        private bool _inGame;
        private PlayerData _playerData;
        private Color32 _defaultColor;
        private IEnumerator _changeColor;
        
        private Material Material => _meshRenderer.material;

        public override void OnStartClient()
        {
            _playerData = Services
                .Container
                .Resolve<IDataProvider>()
                .GetPlayerData();

            _defaultColor = Material.color;
            SetActive(false);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if(isLocalPlayer == false) return;
            if(_inGame == false) return;
            
            ChangeColor(other.gameObject);
        }

        public void SetActive(bool isActive) => _inGame = isActive;

        private void ChangeColor(GameObject other)
        {
            if (other.TryGetComponent(out PlayerSnatch playerSnatch))
            {
                if(playerSnatch.IsSnatching == false) return;
                CommandChangeColor();
            }
        }

        [Command]
        private void CommandChangeColor() => RpcChangeColor();

        [ClientRpc]
        private void RpcChangeColor()
        {
            if(_changeColor != null) 
                StopCoroutine(_changeColor);

            _changeColor = ChangeColorFor(_playerData.ChangedColorDuration);
            StartCoroutine(_changeColor);
        }

        private IEnumerator ChangeColorFor(float duration)
        {
            var wait = new WaitForSeconds(duration);
            Material.color = _playerData.TriggeredColor;
            yield return wait;
            Material.color = _defaultColor;
        }
    }
}