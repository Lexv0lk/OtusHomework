using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private PauseScreenPlaceholder pauseScreenPlaceholder;

        private void OnEnable()
        {
            if (pauseScreenPlaceholder.IsReady == false)
            {
                this.pauseScreenPlaceholder.Spawned += OnPauseScreenSpawned;
            }
            else
            {
                this.button.onClick.AddListener(this.pauseScreenPlaceholder.UI.Show);
            }
        }

        private void OnDisable()
        {
            this.pauseScreenPlaceholder.Spawned -= OnPauseScreenSpawned;
            
            if (pauseScreenPlaceholder.IsReady)
                this.button.onClick.RemoveListener(this.pauseScreenPlaceholder.UI.Show);
        }

        private void OnPauseScreenSpawned(PauseScreen pauseScreen)
        {
            this.pauseScreenPlaceholder.Spawned -= OnPauseScreenSpawned;
            this.button.onClick.AddListener(this.pauseScreenPlaceholder.UI.Show);
        }
    }
}