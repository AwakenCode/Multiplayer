using UnityEngine;

namespace Common
{
    public class SpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public Color Color { get; private set; } = new Color32(255, 100, 255, 255);
        [field: SerializeField] public float Radius { get; private set; } = 0.5f;
    }
}