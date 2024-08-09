using SaveSystem.SaveLoaders;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SaveLoadingEditorController : MonoBehaviour
    {
        private SaveLoadingController _saveLoadingController;
        
        [Inject]
        private void Construct(SaveLoadingController saveLoadingController)
        {
            _saveLoadingController = saveLoadingController;
        }

        [Button]
        private void Save()
        {
            _saveLoadingController.Save();
        }

        [Button]
        private void Load()
        {
            _saveLoadingController.Load();
        }
    }
}