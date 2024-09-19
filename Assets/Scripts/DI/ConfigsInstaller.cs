using Chests.Configs;
using Time.Configs;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "DI/ConfigsInstaller", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ServerTimeConfig _serverTimeConfig;
        [SerializeField] private ChestConfig[] _chestConfigs;

        public override void InstallBindings()
        {
            Container.BindInstance(_serverTimeConfig).AsSingle();
            Container.BindInstance(_chestConfigs).AsCached();

            Container.Bind<ChestConfigList>().AsSingle();
        }
    }
}