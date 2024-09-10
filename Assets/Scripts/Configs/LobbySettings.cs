using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Lobby Settings", menuName = "Configs/Lobby Settings")]
    public class LobbySettings : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)] private float _lowHealthThreshold = 0.2f;
        
        public float LowHealthThreshold => _lowHealthThreshold;
    }
}