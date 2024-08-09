using UnityEngine;

namespace GameEngine.Configs
{
    [CreateAssetMenu(fileName = "Unit Prefab Config", menuName = "Units/Prefab Config")]
    public class UnitPrefabConfig : ScriptableObject
    {
        [SerializeField] private string _unitType;
        [SerializeField] private Unit _prefab;

        public string Type => _unitType;
        public Unit Prefab => _prefab;
    }
}