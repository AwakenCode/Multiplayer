using Common;
using Service.Asset;
using Service.Data;
using UI;
using UnityEngine;

namespace Service.Factory
{
    public class UIFactory : IService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IDataProvider dataProvider, IAssetProvider assetProvider)
        {
            _dataProvider = dataProvider;
            _assetProvider = assetProvider;
        }

        public PlayerLabelUI CreatePlayerLabel(Transform parent)
        {
            var template = _assetProvider.Load(Constants.PlayerUILabelTemplatePath);
            return Object
                .Instantiate(template, parent)
                .GetComponent<PlayerLabelUI>();
        }

        public MainMenu CreateMainMenu()
        {
            var template = _assetProvider.Load(Constants.MainMenuTemplatePath);
            return Object
                .Instantiate(template)
                .GetComponent<MainMenu>();
        }

        public ConnectionWindow CreateConnectionWindow()
        {
            var template = _assetProvider.Load(Constants.ConnectionWindowPath);
            return Object
                .Instantiate(template)
                .GetComponent<ConnectionWindow>();
        }
    }
}