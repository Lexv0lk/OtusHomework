using System.Collections.Generic;
using Game.Scripts.Components;

namespace Game.Scripts.Entities
{
    [System.Serializable]
    public class CharacterCore
    {
        public SimpleMoveComponent MoveComponent;
        public RotateComponent RotateComponent;
        public LifeComponent LifeComponent;

        private readonly HashSet<Component> _components = new();

        public void Compose()
        {
            _components.Add(MoveComponent);
            _components.Add(RotateComponent);
            _components.Add(LifeComponent);

            foreach (var component in _components)
                component.Compose();
            
            MoveComponent.AppendCondition(IsAlive);
            RotateComponent.AppendCondition(IsAlive);
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

        private bool IsAlive()
        {
            return LifeComponent.IsDead.Value == false;
        }
    }
}