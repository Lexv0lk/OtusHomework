using UnityEngine;

namespace Game.Scripts.Configs.Input
{
    [CreateAssetMenu(fileName = "Input Config", menuName = "Configs/Input")]
    public class InputConfig : ScriptableObject
    {
        [SerializeField] private KeyCode _leftKey;
        [SerializeField] private KeyCode _rightKey;
        [SerializeField] private KeyCode _upKey;
        [SerializeField] private KeyCode _downKey;

        public KeyCode Left => _leftKey;
        public KeyCode Right => _rightKey;
        public KeyCode Up => _upKey;
        public KeyCode Down => _downKey;
    }
}