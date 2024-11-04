using Cysharp.Threading.Tasks;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        private readonly SceneLoader _sceneLoader;

        public MenuLoader(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void LoadMenu()
        {
            _sceneLoader.LoadScene("Assets/Game/Scenes/Menu.unity").Forget();
        }
    }
}