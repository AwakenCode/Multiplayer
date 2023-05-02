using Mirror;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Player/Data", order = 61)]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SnatchFactor { get; private set; }
        [field: SerializeField] public float SnatchDuration { get; private set; }
        [field: SerializeField] public float SnatchCooldown { get; private set; }
        [field: SerializeField] public Color32 TriggeredColor { get; private set; }
        [field: SerializeField] public float ChangedColorDuration { get; private set; }

        [field: SyncVar] public string Nickname { get; set; }
    }
}