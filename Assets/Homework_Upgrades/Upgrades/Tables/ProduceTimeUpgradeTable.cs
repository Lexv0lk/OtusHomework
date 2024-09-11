using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Tables
{
    [Serializable]
    public sealed class ProduceTimeUpgradeTable
    {
        [SerializeField] private float _startProduceTime;
        [SerializeField] private float _endProduceTime;

        [ReadOnly] 
        [SerializeField] 
        private float _produceTimeStep;

        [ReadOnly]
        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [SerializeField] 
        private float[] _levels;

        public float GetProduceTime(int level)
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
            _levels = new float[maxLevel];
            _levels[0] = _startProduceTime;
            _levels[maxLevel - 1] = _endProduceTime;

            float produceTimeStep = (_endProduceTime - _startProduceTime) / (maxLevel - 1);
            _produceTimeStep = (float)Math.Round(produceTimeStep, 2);

            for (var i = 1; i < maxLevel - 1; i++)
            {
                float produceTime = _startProduceTime + _produceTimeStep * i;
                _levels[i] = (float)Math.Round(produceTime, 2);
            }
        }

        private void DrawLabelForListElement(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }
    }
}