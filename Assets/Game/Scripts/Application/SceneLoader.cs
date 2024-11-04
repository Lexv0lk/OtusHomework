using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SampleGame
{
    public class SceneLoader
    {
        public async UniTask LoadScene(string scenePath)
        {
            var handle = Addressables.LoadSceneAsync(scenePath);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Failed)
                Debug.LogError($"Failed to load scene: {scenePath}");
        }
    }
}