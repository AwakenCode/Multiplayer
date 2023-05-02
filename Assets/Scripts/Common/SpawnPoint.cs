using UnityEngine;

namespace Common
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Color _color = new Color32(255, 100, 255, 255);
        [SerializeField] public float _radius = 0.5f;
        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}