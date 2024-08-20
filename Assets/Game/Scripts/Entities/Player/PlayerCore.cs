using System;
using System.Collections.Generic;
using Game.Scripts.Components;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerCore
    {
        public ShootComponent ShootComponent;
        
        private readonly HashSet<Component> _components = new();

        public void Compose()
        {
            _components.Add(ShootComponent);

            foreach (var component in _components)
                component.Compose();
        }

        public void Update(float deltaTime)
        {
            foreach (var component in _components)
                component.Update(deltaTime);
        }

        public void Dispose()
        {
            foreach (var component in _components)
                component.Dispose();
        }
    }
}