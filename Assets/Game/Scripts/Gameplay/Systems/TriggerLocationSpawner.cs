using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public class TriggerLocationSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _locationRoot;
        [SerializeField] private AssetReference _locationAsset;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_locationAsset.IsValid())
                return;

            SpawnLocationAsync().Forget();
        }

        private async UniTaskVoid SpawnLocationAsync()
        {
            await _locationAsset.LoadAssetAsync<GameObject>().Task;
            Instantiate(_locationAsset.Asset, _locationRoot);
        }

        private void OnDestroy()
        {
            if (_locationAsset.IsValid())
                _locationAsset.ReleaseAsset();
        }
    }
}