using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu(fileName = "VFX Config", menuName = "Configs/VFX")]
    public class VFXConfig : ScriptableObject
    {
        [SerializeField] private Entity _bloodVFX;
        [SerializeField] private Entity _fireVFX;

        public Entity BloodVFX => _bloodVFX;
        public Entity FireVFX => _fireVFX;
    }
}