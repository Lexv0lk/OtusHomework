using System;
using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(Entity))]
    public class RangeAttackAnimationHandler : MonoBehaviour
    {
        private readonly string _rangeAttackEventName = "RangeAttacked";
        
        [SerializeField] private WeaponInfo _weaponInfo;
        [SerializeField] private AnimatorEventsDispatcher _eventsDispatcher;

        private Entity _entity;

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }
        
        private void OnEnable()
        {
            _eventsDispatcher.SubscribeOnEvent(_rangeAttackEventName, OnRangeAttackAnimated);
        }
        
        private void OnDisable()
        {
            _eventsDispatcher.UnsubcsribeFromEvent(_rangeAttackEventName, OnRangeAttackAnimated);
        }
        
        private void OnRangeAttackAnimated()
        {
            EcsStartup.Instance.CreateEntity(EcsWorlds.EVENTS)
                .Add(new RangeAttackRequest())
                .Add(new SourceEntity() { Value = EcsStartup.Instance.GetWorld().PackEntity(_entity.Id) })
                .Add(new Position() { Value = _weaponInfo.ShootingPoing.position })
                .Add(new Prefab() { Value = _weaponInfo.BulletPrefab });
        }

        [Serializable]
        private struct WeaponInfo
        {
            public Entity BulletPrefab;
            public Transform ShootingPoing;
        }
    }
}