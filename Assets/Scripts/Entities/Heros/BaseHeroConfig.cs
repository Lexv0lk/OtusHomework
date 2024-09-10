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
        
        [Header("Sounds")]
        [SerializeField] private AudioClip[] _abilityClips;
        [SerializeField] private AudioClip[] _deathClips;
        [SerializeField] private AudioClip[] _lowHealthClips;
        [SerializeField] private AudioClip[] _startTurnClips;
        
        protected override void SetupComponents()
        {
            Add(new StatsComponent(_attack, _health));
            Add(new PresentationDataComponent(_icon));
            Add(new SoundsComponent(_abilityClips, _deathClips, _lowHealthClips, _startTurnClips));
        }
    }
}