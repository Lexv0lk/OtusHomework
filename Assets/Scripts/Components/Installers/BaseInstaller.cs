using Client.Common;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private float _health;
        [SerializeField] private Team _team;
        [SerializeField] private Transform _baseEnter;
        [SerializeField] private Transform[] _firePositions;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Health() { CurrentHealth = _health, MaxHealth = _health });
            entity.AddData(new TeamData() { Value = _team });
            entity.AddData(new Position() { Value = _baseEnter.position});
            entity.AddData(GetHarmData());
        }

        private BuildingHarmViewData GetHarmData()
        {
            HarmedPosition[] harmedPositions = new HarmedPosition[_firePositions.Length];
            float step = 1f / _firePositions.Length;
            
            for (int i = 0; i < _firePositions.Length; i++)
            {
                harmedPositions[i] = new HarmedPosition
                {
                    Position = _firePositions[i].position,
                    HealthPart = 1 - i * step
                };
            }
            
            return new BuildingHarmViewData
            {
                CurrentPosIndex = 0,
                Positions = harmedPositions
            };
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}