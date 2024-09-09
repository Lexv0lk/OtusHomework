using Entities;
using Entities.Components;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Sample", menuName = "Entities/Sample")]
    public class BaseHeroConfig : SOEntity
    {
        [SerializeField] private int _attack;
        [SerializeField] private int _health;
        [SerializeField] private Sprite _icon;

        private void OnValidate()
        {
            RemoveAll();
            Add(new StatsComponent(_attack, _health));
            Add(new PresentationDataComponent(_icon));
        }
    }
}