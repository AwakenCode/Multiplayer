using Mirror;
using Service;
using Service.Asset;

namespace Common
{
    public class CustomNetworkManager : NetworkManager
    {
        private PlayerSpawner _playerSpawner;

        public override void Start()
        {
            playerPrefab = Services
                .Container
                .Resolve<IAssetProvider>()
                .Load(Constants.PlayerTemplatePath);
            _playerSpawner = Services.Container.Resolve<PlayerSpawner>();
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient connection)
        {
            _playerSpawner.SpawnFor(connection);
        }
    }
}