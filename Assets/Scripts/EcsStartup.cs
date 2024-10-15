using Client.Components;
using Client.Configs;
using Client.Services;
using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace Client
{
    internal sealed class EcsStartup : MonoSingletone<EcsStartup>
    {
        [SerializeField] private VFXConfig _vfxConfig;
        [SerializeField] private PoolService _poolService;
        [SerializeField] private GameObject _endGameView;
        [SerializeField] private Entity[] _bases;
        
        private EcsWorld _world;
        private EcsWorld _events;
        private IEcsSystems _systems;
        private EntityManager _entityManager;
        private EndGameController _endGameController;
        
        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(GetWorld(worldName));
        }
        
        public EcsWorld GetWorld(string worldName = null)
        {
            return _systems.GetWorld(worldName);
        }

        protected override void Awake()
        {
            base.Awake();
            _entityManager = new EntityManager(_poolService);
            _endGameController = new EndGameController(_entityManager, _endGameView, _bases);
            _world = new EcsWorld();
            _events = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            _systems
                .Add(new LifeTimeLimitationSystem())
                .Add(new NoParentDestroySystem())
                .Add(new ReloadTimeUpdateSystem())
                .Add(new RangeAttackRequestSystem())
                .Add(new MeleeAttackRequestSystem())
                .Add(new SpawnRequestSystem())
                .Add(new BulletCollisionRequestSystem())
                .Add(new TakeDamageRequestSystem())
                .Add(new HealthDeathSystem())
                .Add(new InactiveDestroySystem())
                
                .Add(new TargetDetectionSystem())
                .Add(new MoveToTargetSystem())
                
                .Add(new LookAtTargetSystem())
                .Add(new MovementSystem())
                .Add(new InvokeAttackAnimationSystem())
                
                .Add(new TransformViewSystem())
                .Add(new SpeedAnimatorViewSystem())
                .Add(new UnitDamageViewSystem())
                .Add(new BuildingsHarmViewSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .DelHere<TakeDamageEvent>(EcsWorlds.EVENTS)
                .DelHere<Inactive>();
        }
        
        private void Start()
        {
            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager, _vfxConfig);
            _systems.Init();
        }

        private void Update()
        {
            // process systems here.
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.
            if (_events != null)
            {
                _events.Destroy();
                _events = null;
            }

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}