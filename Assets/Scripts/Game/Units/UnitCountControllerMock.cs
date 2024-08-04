using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class UnitCountControllerMock : MonoBehaviour
    {
        private UnitManager _unitManager;

        [Inject]
        private void Construct(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }
        
        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            return _unitManager.SpawnUnit(prefab, position, rotation);
        }

        [Button]
        public void DestroyUnit(Unit unit)
        {
            _unitManager.DestroyUnit(unit);
        }
    }
}