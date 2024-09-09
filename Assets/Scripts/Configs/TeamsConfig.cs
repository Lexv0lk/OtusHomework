using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Teams Config", menuName = "Configs/Teams")]
    public class TeamsConfig : ScriptableObject
    {
        [SerializeField] private List<EntityConfig> _blueTeam = new();
        [SerializeField] private List<EntityConfig> _redTeam = new();
        
        public List<EntityConfig> BlueTeam => _blueTeam;
        public List<EntityConfig> RedTeam => _redTeam;
    }
}