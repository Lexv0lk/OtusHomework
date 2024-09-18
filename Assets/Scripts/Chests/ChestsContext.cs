using Chests.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Chests
{
    public class ChestsContext : MonoBehaviour
    {
        private ChestsController _chestsController;
        
        [Inject]
        private void Construct(ChestsController chestsController)
        {
            _chestsController = chestsController;
        }

        [Button]
        private void AddChest(ChestConfig config)
        {
            _chestsController.AddChest(config);
        }
    }
}