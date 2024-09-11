using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Tables
{
    [Serializable]
    public class LoadStorageUpgradeTable
    {
        [SerializeField] private int _startLoadStorage;
        [SerializeField] private int _loadStep;

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

        public void Validate(int maxLevel)
        {
            
        }
        
        private void EvaluateTable(int maxLevel)
        {
            var levels = new int[maxLevel];

            var currentHitPoints = this.startHitPoints;
            for (var i = 0; i < maxLevel; i++)
            {
                levels[i] = currentHitPoints;
                currentHitPoints += this.hitPointsStep;
            }

            _levels = levels;
        }
    }
}