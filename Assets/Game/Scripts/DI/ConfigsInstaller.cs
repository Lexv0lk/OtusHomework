using Game.Scripts.Configs.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "DI/Configs Installer")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private MouseRotationConfig _mouseRotationConfig;

        public override void InstallBindings()
        {
            Container.Bind<InputConfig>().FromInstance(_inputConfig).AsCached();
            Container.Bind<MouseRotationConfig>().FromInstance(_mouseRotationConfig).AsCached();
        }
    }
}