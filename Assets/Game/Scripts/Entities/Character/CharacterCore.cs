using System.Collections.Generic;
using Game.Scripts.Components;

namespace Game.Scripts.Entities.Character
{
    [System.Serializable]
    public class CharacterCore
    {
        public MoveComponent MoveComponent;
        public RotateComponent RotateComponent;

        private readonly HashSet<Component> _components = new();

        public void Compose()
        {
            _components.Add(MoveComponent);
            _components.Add(RotateComponent);

            foreach (var component in _components)
                component.Compose();
        }

        public void Update(float deltaTime)
        {
            foreach (var component in _components)
                component.Update(deltaTime);
        }
    }
}