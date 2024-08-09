using System.Collections.Generic;
using GameEngine;
using GameEngine.Structs;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class UnitsSaveLoader : SaveLoader<UnitsSave, UnitManager>
    {
        private readonly UnitPrefabController _unitPrefabController;

        public UnitsSaveLoader(UnitPrefabController unitPrefabController)
        {
            _unitPrefabController = unitPrefabController;
        }
    
        protected override UnitsSave GetSaveData(UnitManager service)
        {
            var units = service.GetAllUnits();
            List<UnitStateSave> unitStateSaves = new List<UnitStateSave>();

            foreach (var unit in units)
                unitStateSaves.Add(GetSaveState(unit));
            
            return new UnitsSave()
            {
                SavedStates = unitStateSaves.ToArray()
            };
        }

        protected override void SetSaveData(UnitManager service, UnitsSave data)
        {
            service.DestroyAllUnits();

            foreach (UnitStateSave state in data.SavedStates)
            {
                Unit prefab = _unitPrefabController.GetUnitPrefab(state.Type);
                Unit spawnedUnit = service.SpawnUnit(prefab, state.Position.ToVector(), Quaternion.Euler(state.Rotation.ToVector()));
                spawnedUnit.HitPoints = state.HitPoints;
            }
        }

        private UnitStateSave GetSaveState(Unit unit)
        {
            return new UnitStateSave()
            {
                Type = unit.Type,
                Position = unit.Position.ToFloat3(),
                Rotation = unit.Rotation.ToFloat3(),
                HitPoints = unit.HitPoints
            };
        }
    }
}