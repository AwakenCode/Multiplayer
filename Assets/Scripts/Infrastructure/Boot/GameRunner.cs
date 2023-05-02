using UnityEngine;

namespace Infrastructure.Boot
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrap _gameBootstrap;

        private void Awake()
        {
            if (FindObjectOfType<GameBootstrap>() != null) return;

            Instantiate(_gameBootstrap);
        }
    }
}