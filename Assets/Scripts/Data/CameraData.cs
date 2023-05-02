using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Camera/Config", order = 61)]
    public class CameraData : ScriptableObject
    {
        [field: SerializeField] public Vector3 NormalPosition { get; private set; }
        [field: SerializeField] public Vector2 RotationSpeed { get; private set; }
        [field: SerializeField] public float SmoothTime { get; private set; }
        [field: SerializeField] public float MovingSpeed { get; private set; }
        [field: SerializeField] public float MinAngle { get; private set; }
        [field: SerializeField] public float MaxAngle { get; private set; }
    }
}