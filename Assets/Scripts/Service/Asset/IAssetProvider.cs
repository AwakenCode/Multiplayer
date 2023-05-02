using UnityEngine;

namespace Service.Asset
{
    public interface IAssetProvider : IService
    {
        GameObject Load(string path);
    }
}