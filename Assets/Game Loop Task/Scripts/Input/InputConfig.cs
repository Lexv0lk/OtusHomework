using UnityEngine;

namespace ShootEmUp.Input
{
    [CreateAssetMenu(
            fileName = "InputConfig",
            menuName = "Input/New InputConfig"
        )]
    public class InputConfig : ScriptableObject
    {
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _right;
        [SerializeField] private KeyCode _shoot;

        public KeyCode Left => _left;
        public KeyCode Right => _right;
        public KeyCode Shoot => _shoot;
    }
}