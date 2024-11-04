using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class Bootstrap : MonoBehaviour
    {
        private UIManager _uiManager;
        private MenuLoader _menuLoader;
        
        [Inject]
        private void Construct(UIManager uiManager, MenuLoader menuLoader)
        {
            _uiManager = uiManager;
            _menuLoader = menuLoader;
        }

        private async void Awake()
        {
            await _uiManager.LoadUIAsync();
            _menuLoader.LoadMenu();
        }
    }
}