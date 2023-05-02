using Data;

namespace Service.Data
{
    public interface IDataProvider : IService
    {
        void Load();
        PlayerData GetPlayerData();
        CameraData GetCameraData();
        LevelData GetLevelData();
    }
}