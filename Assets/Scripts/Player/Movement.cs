using Data;
using Mirror;
using Service;
using Service.Data;
using Service.Input;
using UnityEngine;

namespace Player
{
    public class Movement : NetworkBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rotationSmoothTime;
        [SerializeField] private PlayerSnatch _playerSnatch;

        private PlayerData _playerData;

        private IInputService _input;
        private Vector3 _direction;
        private Vector3 _motion;
        private float _currentVelocity;

        private void Awake()
        {
            _input = Services.Container.Resolve<IInputService>();
            _playerData = Services.Container.Resolve<IDataProvider>().GetPlayerData();
            SetActiveGravity(false);
        }
        
        private void Update()
        {
            if (isOwned == false) return;
            if (_playerSnatch.IsSnatching) return;

            Move();
            Rotate();
        }

        public void SetActiveGravity(bool isActive)
        {
            if (isActive)
            {
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
                return;
            }

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        private void Move()
        {
            _direction = new Vector3(_input.Movement.x, _direction.y, _input.Movement.y);
            _motion = _playerData.Speed * Time.deltaTime * _direction;
            transform.position += _motion;
        }

        private void Rotate()
        {
            if (_direction.x == 0 && _direction.z == 0) return;

            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}