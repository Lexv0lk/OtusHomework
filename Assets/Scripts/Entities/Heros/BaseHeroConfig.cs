using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Sample", menuName = "Entities/Sample")]
    public class BaseHeroConfig : EntityConfig
    {
        [Header("Stats")]
        [SerializeField] private int _attack;
        [SerializeField] private int _health;
        
        [Header("Presentation")]
        [SerializeField] private Sprite _icon;
        
        protected override void SetupComponents()
        {
            Add(new StatsComponent(_attack, _health));
            Add(new PresentationDataComponent(_icon));
        }
    }
}