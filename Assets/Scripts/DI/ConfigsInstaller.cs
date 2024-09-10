using Configs;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "Installers/Configs Installer")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private TeamsConfig _teamsConfig;
        [SerializeField] private LobbySettings _lobbySettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_teamsConfig).AsSingle();
            Container.BindInstance(_lobbySettings).AsSingle();
        }
    }
}