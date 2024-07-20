using ShootEmUp.Bullets;
using ShootEmUp.Characters;
using ShootEmUp.Enemies;
using ShootEmUp.Level;
using UnityEngine;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    public class SceneObjectsInstaller : MonoInstaller
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private Character _player;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private EnemyPool _enemyPool;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelBounds>().FromInstance(_levelBounds);
            Container.BindInterfacesAndSelfTo<Character>().FromInstance(_player).AsCached();
            Container.BindInterfacesAndSelfTo<EnemyPositions>().FromInstance(_enemyPositions);
            Container.BindInterfacesAndSelfTo<BulletPool>().FromInstance(_bulletPool);
            Container.BindInterfacesAndSelfTo<EnemyPool>().FromInstance(_enemyPool);
        }
    }
}