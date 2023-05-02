using Common;
using Data;
using UnityEngine;

namespace Service.Data
{
    public class DataProvider : IDataProvider
    {
        private PlayerData _playerData;
        private CameraData _cameraData;
        private LevelData _levelData;

        public void Load()
        {
            _playerData = Resources.Load<PlayerData>(Constants.PlayerDataPath);
            _cameraData = Resources.Load<CameraData>(Constants.CameraDataPath);
            _levelData = Resources.Load<LevelData>(Constants.LevelDataPath);
        }

        public PlayerData GetPlayerData() => _playerData;
        public CameraData GetCameraData() => _cameraData;
        public LevelData GetLevelData() => _levelData;
    }
}