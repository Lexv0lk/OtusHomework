using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Orc", menuName = "Entities/Orc")]
    public class Orc : BaseHeroConfig
    {
        [Header("Random Target")]
        [SerializeField, Range(0f, 1f)] private float _randomTargetChance = 0.5f;
        
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new RandomTargetComponent(_randomTargetChance));
        }
    }
}