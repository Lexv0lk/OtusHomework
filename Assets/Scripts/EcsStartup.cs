using AB_Utility.FromSceneToEntityConverter;
using Client.Components;
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
        private EcsWorld _world;
        private EcsWorld _events;
        private IEcsSystems _systems;
        private EntityManager _entityManager;
        
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
            _entityManager = new EntityManager();
            _world = new EcsWorld();
            _events = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            _systems
                .Add(new ReloadTimeUpdateSystem())
                .Add(new MeleeAttackRequestSystem())
                .Add(new TakeDamageRequestSystem())
                
                .Add(new TargetDetectionSystem())
                .Add(new MoveToTargetSystem())
                
                .Add(new LookAtTargetSystem())
                .Add(new MovementSystem())
                .Add(new InvokeAttackAnimationSystem())
                
                .Add(new TransformViewSystem())
                .Add(new SpeedAnimatorViewSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .DelHere<TakeDamageEvent>();
        }
        
        private void Start()
        {
            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager);
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