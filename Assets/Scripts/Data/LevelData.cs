using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Level/Data", order = 61)]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public List<Vector3> SpawnPoints;
    }
}