using System.Collections.Generic;
using UnityEngine;

namespace Service.Asset
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, GameObject> _templates = new();

        public GameObject Load(string path)
        {
            if (_templates.TryGetValue(path, out var template))
                return template;
            
            var source = Resources.Load<GameObject>(path);
            _templates.Add(path, source.gameObject);
            return source.gameObject;
        }
    }
}