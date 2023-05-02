using Common;
using Service.Asset;
using Service.Data;
using Service.Network;
using UnityEngine;

namespace Service.Factory
{
    public class CommonFactory : IService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IAssetProvider _assetProvider;

        public CommonFactory(IDataProvider dataProvider, IAssetProvider assetProvider)
        {
            _dataProvider = dataProvider;
            _assetProvider = assetProvider;
        }

        public Player.Player CreatePlayer()
        {
            var template = _assetProvider.Load(Constants.PlayerTemplatePath);
            return Object
                .Instantiate(template)
                .GetComponent<Player.Player>();
        }

        public MatchMaker CreateMatchMaker()
        {
            var template = _assetProvider.Load(Constants.MatchMakerTemplatePath);
            return Object
                .Instantiate(template)
                .GetComponent<MatchMaker>();
        }

        public CustomNetworkManager CreateNetworkManager()
        {
            var template = _assetProvider.Load(Constants.NetworkManagerTemplatePath);
            return Object
                .Instantiate(template)
                .GetComponent<CustomNetworkManager>();
        }
        
        public Updater CreateUpdater()
        {
            var template = _assetProvider.Load(Constants.UpdaterTemplatePath);
            return Object
                .Instantiate(template)
                .GetComponent<Updater>();
        }
    }
}