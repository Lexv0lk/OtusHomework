﻿using System;
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

        public int GetLoadStorage(int level)
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
            int currentLoadStorage = _startLoadStorage;
            
            for (var i = 0; i < maxLevel; i++)
            {
                levels[i] = currentLoadStorage;
                currentLoadStorage += _loadStep;
            }

            _levels = levels;
        }
    }
}