using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Teams Config", menuName = "Configs/Teams")]
    public class TeamsConfig : ScriptableObject
    {
        [SerializeField] private SOEntity[] _blueTeam;
        [SerializeField] private SOEntity[] _redTeam;
        
        public IReadOnlyList<SOEntity> BlueTeam => _blueTeam;
        public IReadOnlyList<SOEntity> RedTeam => _redTeam;
    }
}