using System.Collections.Generic;
using Configs;
using DI;
using Entities;
using Entities.Components;
using UI;
using Zenject;

namespace Controllers
{
    public class TeamSetupController : IInitializable
    {
        private readonly TeamsConfig _teamsConfig;
        private readonly TeamViewSetup _teamViewSetup;

        public TeamSetupController(TeamsConfig teamsConfig, TeamViewSetup teamViewSetup)
        {
            _teamsConfig = teamsConfig;
            _teamViewSetup = teamViewSetup;
        }

        public void Initialize()
        {
            SetupTeamView(_teamsConfig.RedTeam, _teamViewSetup.RedTeam);
            SetupTeamView(_teamsConfig.BlueTeam, _teamViewSetup.BlueTeam);
        }

        private void SetupTeamView(IReadOnlyList<SOEntity> entities, HeroListView views)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                
                if (entity.TryGet<BaseViewComponent>(out var viewComponent))
                    views.GetView(i).SetIcon(viewComponent.Icon);
                
                if (entity.TryGet<StatsComponent>(out var statsComponent))
                    views.GetView(i).SetStats($"{statsComponent.Attack} / {statsComponent.Health}");
            }
        }
    }
}