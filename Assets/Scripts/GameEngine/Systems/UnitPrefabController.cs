using System.Collections.Generic;
using GameEngine.Configs;

namespace GameEngine
{
    public class UnitPrefabController 
    {
        private readonly Dictionary<string, Unit> _unitPrefabs = new();

        public UnitPrefabController(List<UnitPrefabConfig> configs)
        {
            foreach (var config in configs)
                _unitPrefabs[config.Type] = config.Prefab;
        }

        public Unit GetUnitPrefab(string unitType)
        {
            return _unitPrefabs[unitType];
        }
    }
}