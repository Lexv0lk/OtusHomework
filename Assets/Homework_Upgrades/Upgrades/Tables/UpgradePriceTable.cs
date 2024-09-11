using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Tables
{
    [Serializable]
    public class UpgradePriceTable
    {
        [SerializeField] private int _basePrice;

        [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")] [ReadOnly] [SerializeField]
        private int[] _levels;

        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }

        public int GetPrice(int level)
        {
            int index = level - 1;
            index = Mathf.Clamp(index, 0, _levels.Length - 1);
            return _levels[index];
        }

        public void Validate(int maxLevel)
        {
            EvaluatePriceTable(maxLevel);
        }

        private void EvaluatePriceTable(int maxLevel)
        {
            var table = new int[maxLevel];
            table[0] = 0;

            for (var level = 2; level <= maxLevel; level++)
            {
                var price = _basePrice * level;
                table[level - 1] = price;
            }

            _levels = table;
        }
    }
}