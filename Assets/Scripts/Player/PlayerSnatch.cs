using System.Collections;
using Data;
using Mirror;
using Service;
using Service.Data;
using Service.Input;
using UnityEngine;

namespace Player
{
    public class PlayerSnatch : NetworkBehaviour
    {
        private IInputService _input;
        private IEnumerator _snatch;
        private IEnumerator _waitSnatchCooldown;
        private PlayerData _playerData;
        private Vector3 _direction;
        private Vector3 _motion;
        private bool _canSnatch = true;

        [field: SyncVar] public bool IsSnatching { get; private set; }

        private void Awake()
        {
            _input = Services.Container.Resolve<IInputService>();
            _playerData = Services.Container.Resolve<IDataProvider>().GetPlayerData();   
            _input.OnLeftMouseButtonClicked += Snatch;
        }

        private void Snatch()
        {
            if (isLocalPlayer == false) return;
            if(_canSnatch == false) return;
            
            if(_snatch != null) 
                StopCoroutine(_snatch);

            if (_waitSnatchCooldown != null)
                StopCoroutine(_waitSnatchCooldown);

            _snatch = SnatchFor(_playerData.SnatchDuration);
            _waitSnatchCooldown = WaitSnatchCooldown(_playerData.SnatchCooldown);
            
            StartCoroutine(_snatch);
            StartCoroutine(_waitSnatchCooldown);
        }

        private IEnumerator SnatchFor(float duration)
        {
            float elapsedTime = 0;
            var waitForEndFrame = new WaitForEndOfFrame();
            
            ChangeState(true);
            
            while (elapsedTime < duration)
            {
                _direction = new Vector3(_input.Movement.x, _direction.y, _input.Movement.y);
                _motion = _playerData.Speed * Time.deltaTime * _playerData.SnatchFactor * _direction;
                transform.position += _motion;
                
                yield return waitForEndFrame;
                elapsedTime += Time.deltaTime;
            }

            ChangeState(false);
        }

        private IEnumerator WaitSnatchCooldown(float cooldownTime)
        {
            var cooldown = new WaitForSeconds(cooldownTime);
            _canSnatch = false;
            yield return cooldown;
            _canSnatch = true;
        }

        [Command]
        private void ChangeState(bool isActive)
        {
            IsSnatching = isActive;
        }
    }
}