using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Vampire", menuName = "Entities/Vampire")]
    public class Vampire : BaseHeroConfig
    {
        [Header("Vampire Attack")]
        [SerializeField, Range(0f, 1f)] private float _vampireAttackChance = 0.5f;
        
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new VampireAttackComponent(_vampireAttackChance));
        }
    }
}