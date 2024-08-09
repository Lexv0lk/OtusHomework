using GameEngine.Configs;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "DI/Configs Installer")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UnitPrefabConfig[] _configs;
        
        public override void InstallBindings()
        {
            foreach (var config in _configs)
                Container.Bind<UnitPrefabConfig>().FromInstance(config).AsCached();
        }
    }
}