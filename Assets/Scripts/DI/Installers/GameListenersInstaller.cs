using ShootEmUp.Common;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    public class GameListenersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            foreach (IGameObjectSpawner spawner in GetComponentsInChildren<IGameObjectSpawner>())
                Container.Bind<IGameObjectSpawner>().FromInstance(spawner);
            
            foreach (IGameStateListener listener in GetComponentsInChildren<IGameStateListener>())
                Container.Bind<IGameStateListener>().FromInstance(listener);
            
            foreach (IGameUpdateListener listener in GetComponentsInChildren<IGameUpdateListener>())
                Container.Bind<IGameUpdateListener>().FromInstance(listener);
        }
    }
}