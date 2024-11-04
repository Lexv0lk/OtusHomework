using UnityEngine;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UIPrefabsConfig _uiPrefabsConfig;
        
        public override void InstallBindings()
        {
            this.Container.BindInstance(_uiPrefabsConfig);

            this.Container.Bind<SceneLoader>().AsSingle();
            this.Container.Bind<UIManager>().AsSingle().NonLazy();
            
            this.Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            this.Container.Bind<GameLoader>().AsSingle().NonLazy();
            this.Container.Bind<MenuLoader>().AsSingle().NonLazy();
        }
    }
}