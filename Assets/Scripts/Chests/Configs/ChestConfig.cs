using System.Collections.Generic;
using Rewards;
using UnityEngine;

namespace Chests.Configs
{
    [CreateAssetMenu(fileName = "Chest Config", menuName = "Configs/New Chest")]
    public class ChestConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _closeDuration;
        [SerializeReference] private IReward[] _rewards;

        public string Name => _name;
        public Sprite Icon => _icon;
        public float CloseDuration => _closeDuration;
        public IEnumerable<IReward> Rewards => _rewards;
    }
}