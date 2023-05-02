using Data;
using Service;
using Service.Data;
using Service.Input;
using UnityEngine;

namespace Camera
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _cameraHolder;
        
        private CameraData _cameraData;
        private IInputService _input;

        private float _lookAngle; 
        private float _tiltAngle;

        private Vector2 _smooth;
        private Vector2 _smoothVelocity;
        
        private void Awake()
        {
            _cameraData = Services.Container.Resolve<IDataProvider>().GetCameraData();
            _input = Services.Container.Resolve<IInputService>();
        }
        
        private void LateUpdate()
        {
            HandlePosition();
            Rotate();
        }
        
        private void HandlePosition()
        {
            float targetX = _cameraData.NormalPosition.x;
            float targetY = _cameraData.NormalPosition.y;
            float targetZ = _cameraData.NormalPosition.z;

            var localPosition = _cameraTransform.localPosition;
            var cameraPosition = localPosition;
            cameraPosition.z = targetZ;

            _pivot.localPosition = new Vector3(targetX, targetY);
            localPosition = Vector3.Lerp(
                localPosition, cameraPosition, _cameraData.MovingSpeed * Time.deltaTime);
            _cameraTransform.localPosition = localPosition;
        }

        private void Rotate()
        {
            if (_cameraData.SmoothTime > 0)
            {
                _smooth.x = Mathf.SmoothDamp(_smooth.x, _input.Rotation.x, ref _smoothVelocity.x, _cameraData.SmoothTime);
                _smooth.y = Mathf.SmoothDamp(_smooth.y, _input.Rotation.y, ref _smoothVelocity.y, _cameraData.SmoothTime);
            }
            else
            {
                _smooth.x = _input.Rotation.x;
                _smooth.y = _input.Rotation.y;
            }

            _lookAngle += _smooth.x * _cameraData.RotationSpeed.x;
            var targetRotation = Quaternion.Euler(0, _lookAngle, 0);
            _cameraHolder.rotation = targetRotation;

            _tiltAngle -= _smooth.y * _cameraData.RotationSpeed.y;
            _tiltAngle = Mathf.Clamp(_tiltAngle, _cameraData.MinAngle, _cameraData.MaxAngle);
            _pivot.localRotation = Quaternion.Euler(_tiltAngle, 0, 0);
        } 
    }
}