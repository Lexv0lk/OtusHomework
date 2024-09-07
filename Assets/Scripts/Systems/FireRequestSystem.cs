using Data;
using Data.Events;
using Unity.Entities;

namespace Systems
{
    public partial class FireRequestSystem : SystemBase
    {
        private float _delay;
        private float _currentDelay;
        private BeginSimulationEntityCommandBufferSystem _ecbs;
        
        protected override void OnCreate()
        {
            _ecbs = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            _delay = GetSingleton<ShootingDelay>().Value;
            _currentDelay = _delay;
        }

        protected override void OnUpdate()
        {
            _currentDelay += Time.DeltaTime;

            if (_currentDelay >= _delay)
            {
                _currentDelay = 0;
                var commandBuffer = _ecbs.CreateCommandBuffer();
                
                Entities.WithAll<WeaponData>().WithNone<FireRequestEvent>().ForEach((Entity entity) =>
                {   
                    commandBuffer.AddComponent(entity, new FireRequestEvent());
                }).Schedule();
                
                _ecbs.AddJobHandleForProducer(Dependency);
            }
        }
    }
}