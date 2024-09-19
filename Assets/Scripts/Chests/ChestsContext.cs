using Chests.Configs;
using Chests.Presenters;
using Chests.View;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Chests
{
    public class ChestsContext : MonoBehaviour
    {
        private ChestsController _chestsController;
        
        [Inject]
        private void Construct(ChestsController chestsController, ChestListView chestListView)
        {
            _chestsController = chestsController;

            var presenter = new ChestListViewPresenter(_chestsController);
            chestListView.Initialize(presenter);
        }

        [Button]
        private void AddChest(ChestConfig config)
        {
            _chestsController.Add(config);
        }
    }
}