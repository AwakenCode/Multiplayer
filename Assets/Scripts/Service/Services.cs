namespace Service
{
    public class Services
    {
        private static Services _instance;
        public static Services Container => _instance ??= new Services();

        public void RegisterSingle<TService>(TService service) where TService : IService => 
            Implementation<TService>.Instance = service;

        public TService Resolve<TService>() where TService : IService => 
            Implementation<TService>.Instance;

        private class Implementation<TService> where TService : IService
        {
            public static TService Instance;
        }
    }
}