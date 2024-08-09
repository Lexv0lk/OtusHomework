using System.Collections.Generic;

namespace DI.Contexts
{
    public class GameContext
    {
        private readonly ServicesContext _servicesContext;
        
        public GameContext(List<IGameService> services)
        {
            _servicesContext = new();

            foreach (var service in services)
                _servicesContext.RegisterService(service);
        }
        
        public T GetService<T>()
        {
            return _servicesContext.GetService<T>();
        } 
    }
}