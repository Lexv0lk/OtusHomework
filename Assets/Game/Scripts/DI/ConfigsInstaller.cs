using Game.Scripts.Configs.Fabrics;
using Game.Scripts.Configs.Input;
using Game.Scripts.Configs.Models;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "DI/Configs Installer")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private MouseRotationConfig _mouseRotationConfig;
        [SerializeField] private BulletFabricConfig _bulletFabricConfig;
        [SerializeField] private RiffleStoreConfig _riffleStoreConfig;

        public override void InstallBindings()
        {
            Container.Bind<InputConfig>().FromInstance(_inputConfig).AsCached();
            Container.Bind<MouseRotationConfig>().FromInstance(_mouseRotationConfig).AsCached();
            Container.Bind<BulletFabricConfig>().FromInstance(_bulletFabricConfig).AsCached();
            Container.Bind<RiffleStoreConfig>().FromInstance(_riffleStoreConfig).AsCached();
        }
    }
}