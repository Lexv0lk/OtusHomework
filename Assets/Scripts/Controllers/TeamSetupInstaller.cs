using System.Collections.Generic;
using Configs;
using DI;
using Entities;
using Entities.Components;
using Models;
using UI;
using Utils;
using Zenject;

namespace Controllers
{
    public class TeamSetupInstaller : IInitializable
    {
        private readonly TeamsConfig _teamsConfig;
        private readonly UIService _uiService;
        private readonly TeamsSetup _teamsSetup;

        public TeamSetupInstaller(TeamsConfig teamsConfig, UIService uiService, TeamsSetup teamsSetup)
        {
            _teamsConfig = teamsConfig;
            _uiService = uiService;
            _teamsSetup = teamsSetup;
        }

        public void Initialize()
        {
            SetupTeamView(_teamsConfig.RedTeam, _uiService.GetRedPlayer(), _teamsSetup.RedTeam, Team.Red);
            SetupTeamView(_teamsConfig.BlueTeam, _uiService.GetBluePlayer(), _teamsSetup.BlueTeam, Team.Blue);
        }

        private void SetupTeamView(IReadOnlyList<EntityConfig> configs, HeroListView views, IteratableList<IEntity> teamList, Team team)
        {
            for (int i = 0; i < configs.Count; i++)
            {
                var entity = configs[i].GetEntityInstance();
                
                if (entity.TryGet<PresentationDataComponent>(out var viewComponent))
                    views.GetView(i).SetIcon(viewComponent.Icon);
                
                if (entity.TryGet<StatsComponent>(out var statsComponent))
                    views.GetView(i).SetStats($"{statsComponent.Attack} / {statsComponent.CurrentHealth}");
                
                entity.Add(new HeroPresentationComponent(views.GetView(i)));
                entity.Add(new TeamComponent(team));

                teamList.Add(entity);
            }
        }
    }
}