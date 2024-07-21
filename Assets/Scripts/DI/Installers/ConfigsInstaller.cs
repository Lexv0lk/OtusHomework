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
            Container.Bind<InputConfig>().FromInstance(_inputConfig);
            Container.Bind<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig);
            Container.Bind<StartScreenConfig>().FromInstance(_startScreenConfig);
        }
    }
}