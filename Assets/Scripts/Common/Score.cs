using Mirror;
using TMPro;
using UnityEngine;

namespace Common
{
    public class Score : NetworkBehaviour
    {
        [SerializeField] private TMP_Text _collisionCountText;

        [SyncVar] private int _collisionCount;

        public void AddCollision()
        {
            _collisionCount++;
            _collisionCountText.text = _collisionCount.ToString();
        }
    }
}