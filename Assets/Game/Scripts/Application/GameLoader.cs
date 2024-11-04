using Cysharp.Threading.Tasks;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly SceneLoader _sceneLoader;

        public GameLoader(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void LoadGame()
        {
            _sceneLoader.LoadScene("Assets/Game/Scenes/Game.unity").Forget();
        }
    }
}