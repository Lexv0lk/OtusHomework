using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Tables
{
    [Serializable]
    public class UnloadStorageUpgradeTable
    {
        [SerializeField] private int _startUnloadStorage;
        [SerializeField] private int _unloadStep;

        [Space]
        [ReadOnly]
        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [SerializeField]
        private int[] _levels;

        private void DrawLabelForListElement(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }

        public int GetUnloadStorage(int level)
        {
            int index = level - 1;
            return _levels[index];
        }

        public void Validate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }
        
        private void EvaluateTable(int maxLevel)
        {
            int[] levels = new int[maxLevel];
            int currentLoadStorage = _startUnloadStorage;
            
            for (var i = 0; i < maxLevel; i++)
            {
                levels[i] = currentLoadStorage;
                currentLoadStorage += _unloadStep;
            }

            _levels = levels;
        }
    }
}