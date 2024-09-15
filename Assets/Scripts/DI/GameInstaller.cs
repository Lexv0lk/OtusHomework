using Lessons.MetaGame.Inventory;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsCached();
        }
    }
}