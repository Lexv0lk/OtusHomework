using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    public class BulletCollisionComponent : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Entity target))
            {
                EcsStartup.Instance.CreateEntity(EcsWorlds.EVENTS)
                    .Add(new CollisionEnterRequest())
                    .Add(new BulletTag())
                    .Add(new SourceEntity { Value = EcsStartup.Instance.GetWorld().PackEntity(_entity.Id) })
                    .Add(new TargetEntity { Value = EcsStartup.Instance.GetWorld().PackEntity(target.Id) })
                    .Add(new Position { Value = other.ClosestPoint(transform.position) });
            }
        }
    }
}