using System.Collections.Generic;
using Mirror;
using Service.Factory;
using UnityEngine;

namespace Service
{
    public class PlayerSpawner : IService
    {
        private readonly List<Vector3> _spawnPoints;
        private readonly CommonFactory _commonFactory;

        private int _nextPlayerSpawnIndex;

        public PlayerSpawner(CommonFactory commonFactory, List<Vector3> spawnPoints)
        {
            _commonFactory = commonFactory;
            _spawnPoints = spawnPoints;
        }

        public void SpawnFor(NetworkConnectionToClient connection)
        {
            var player = _commonFactory.CreatePlayer();
            player.transform.SetPositionAndRotation(_spawnPoints[_nextPlayerSpawnIndex], Quaternion.identity);
            NetworkServer.AddPlayerForConnection(connection, player.gameObject);
            _nextPlayerSpawnIndex++;
        }    
    }
}