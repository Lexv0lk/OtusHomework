using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(Entity))]
    public class MeleeAttackAnimationHandler : MonoBehaviour
    {
        private readonly string _meleeAttackEventName = "MeleeAttacked";
        
        [SerializeField] private AnimatorEventsDispatcher _eventsDispatcher;
        
        private Entity _entity;

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        private void OnEnable()
        {
            _eventsDispatcher.SubscribeOnEvent(_meleeAttackEventName, OnMeleeAttackAnimated);
        }

        private void OnDisable()
        {
            _eventsDispatcher.UnsubcsribeFromEvent(_meleeAttackEventName, OnMeleeAttackAnimated);
        }

        private void OnMeleeAttackAnimated()
        {
            EcsStartup.Instance.CreateEntity(EcsWorlds.EVENTS)
                .Add(new MeleeAttackRequest())
                .Add(new SourceEntity() { Value = EcsStartup.Instance.GetWorld().PackEntity(_entity.Id) });
        }
    }
}