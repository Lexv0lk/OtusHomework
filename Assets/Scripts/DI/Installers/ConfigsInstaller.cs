using ShootEmUp.Enemies;
using ShootEmUp.Input;
using ShootEmUp.StartScreen;
using UnityEngine;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "Installers/Configs")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private StartScreenConfig _startScreenConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputConfig>().FromInstance(_inputConfig);
            Container.BindInterfacesAndSelfTo<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig);
            Container.BindInterfacesAndSelfTo<StartScreenConfig>().FromInstance(_startScreenConfig);
        }
    }
}