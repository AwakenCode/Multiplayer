using Common;
using Infrastructure.Boot;
using Infrastructure.States.StateMachine;
using Service;
using Service.Asset;
using Service.Data;
using Service.Factory;
using Service.Input;
using Service.Network;
using UI;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Services _services;
        private readonly GameStateMachine _gameStateMachine;

        private IDataProvider _dataProvider;
        private IAssetProvider _assetProvider;
        private Updater _updater;
        private SceneLoader _sceneLoader;
        private UIFactory _uiFactory;
        private CommonFactory _commonFactory;
        private ConnectionWindow _connectionWindow;
        
        public BootstrapState(ICoroutineRunner coroutineRunner, Services services, GameStateMachine gameStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _services = services;
            _gameStateMachine = gameStateMachine;
            
            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load(Constants.BootSceneName, onLoaded: OnLoaded);

        public void Exit() => _connectionWindow.OnEnter -= NextState;
        
        private void OnLoaded()
        {
            _connectionWindow = _uiFactory.CreateConnectionWindow();
            _connectionWindow.OnEnter += NextState;
        }

        private void NextState() => _gameStateMachine.Enter<MainMenuState>();

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterDataProvider();
            RegisterCoroutineRunner();
            RegisterNetworkPlayer();
            RegisterSceneLoader();
            RegisterCommonFactory();
            RegisterUIFactory();
            RegisterUpdater();
            RegisterInputService();
            RegisterPlayerSpawner();
            RegisterMatchMaker();
            RegisterNetworkService();
        }

        private void RegisterNetworkPlayer() => _services.RegisterSingle(new NetworkPlayer());

        private void RegisterCoroutineRunner() => _services.RegisterSingle(_coroutineRunner);

        private void RegisterAssetProvider()
        {
            _assetProvider = new AssetProvider();
            _services.RegisterSingle(_assetProvider);
        }

        private void RegisterUpdater()
        {
            _updater = _commonFactory.CreateUpdater();
            _sceneLoader.AddDontDestroyObject(_updater.gameObject);
            _services.RegisterSingle(_updater);
        }

        private void RegisterPlayerSpawner()
        {
            var spawnPoints = _dataProvider.GetLevelData().SpawnPoints;
            _services.RegisterSingle(new PlayerSpawner(_commonFactory, spawnPoints));
        }

        private void RegisterMatchMaker()
        {
            // var matchMaker = _commonFactory.CreateMatchMaker();
            // _sceneLoader.AddDontDestroyObject(matchMaker.gameObject);
            _services.RegisterSingle(new MatchMaker());
        }

        private void RegisterNetworkService()
        {
            var networkManager = _commonFactory.CreateNetworkManager();
            
            _services.RegisterSingle(new NetworkService(
                networkManager, _services.Resolve<MatchMaker>()
            ));
        }

        private void RegisterSceneLoader()
        {
            _sceneLoader = new SceneLoader(_coroutineRunner);
            _services.RegisterSingle(_sceneLoader);
        }

        private void RegisterInputService()
        {
            var input = new StandaloneInput();
            _updater.AddListeners(input);
            _services.RegisterSingle<IInputService>(input);
        }

        private void RegisterCommonFactory()
        {
            _commonFactory = new CommonFactory(_dataProvider, _assetProvider);
            _services.RegisterSingle(_commonFactory);
        }

        private void RegisterUIFactory()
        {
            _uiFactory = new UIFactory(_dataProvider, _assetProvider);;
            _services.RegisterSingle(_uiFactory);
        }

        private void RegisterDataProvider()
        {
            _dataProvider = new DataProvider();
            _dataProvider.Load();
            _services.RegisterSingle(_dataProvider);
        }
    }
}