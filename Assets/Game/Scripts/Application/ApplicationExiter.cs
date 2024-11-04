using UnityEditor;

namespace SampleGame
{
    public sealed class ApplicationExiter
    {
        private readonly UIManager _uiManager;

        public ApplicationExiter(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public void ExitApp()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}