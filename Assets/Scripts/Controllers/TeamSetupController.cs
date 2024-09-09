using System.Collections.Generic;
using Configs;
using DI;
using Entities;
using Entities.Components;
using UI;
using Utils;
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
            SetupTeamView(_teamsConfig.RedTeam, _teamViewSetup.RedTeam, Team.Red);
            SetupTeamView(_teamsConfig.BlueTeam, _teamViewSetup.BlueTeam, Team.Blue);
        }

        private void SetupTeamView(IReadOnlyList<SOEntity> entities, HeroListView views, Team team)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                
                if (entity.TryGet<PresentationDataComponent>(out var viewComponent))
                    views.GetView(i).SetIcon(viewComponent.Icon);
                
                if (entity.TryGet<StatsComponent>(out var statsComponent))
                    views.GetView(i).SetStats($"{statsComponent.Attack} / {statsComponent.Health}");
                
                entity.Add(new HeroPresentationComponent(views.GetView(i)));
                entity.Add(new TeamComponent(team));
            }
        }
    }
}