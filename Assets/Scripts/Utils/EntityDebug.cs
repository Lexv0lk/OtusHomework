using Entities;
using Entities.Components;
using UnityEngine;

namespace Utils
{
    public static class EntityDebug
    {
        public static void Log(IEntity entity, string action = "")
        {
            if (entity.TryGet<HeroPresentationComponent>(out var heroPresentationComponent))
            {
                var view = heroPresentationComponent.View;
                string name = view.gameObject.name;
                
                Debug.Log($"{name} {action}");
            }
        }
    }
}