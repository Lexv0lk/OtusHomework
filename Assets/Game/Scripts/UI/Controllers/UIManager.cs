using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public class UIManager : IDisposable
    {
        private readonly AssetReference[] _uiPrefabs;
        private readonly Dictionary<string, GameObject> _loadedPrefabs = new();

        public UIManager(UIPrefabsConfig uiPrefabsConfig)
        {
            _uiPrefabs = uiPrefabsConfig.Prefabs;
        }
        
        public async UniTask LoadUIAsync()
        {
            foreach (var uiPrefab in _uiPrefabs)
            {
                var prefabAsset = await uiPrefab.LoadAssetAsync<GameObject>().Task;
                _loadedPrefabs[uiPrefab.AssetGUID] = prefabAsset;
            }
        }
        
        public GameObject GetUIPrefab(string uiPrefabGuid)
        {
            return _loadedPrefabs[uiPrefabGuid];
        }
        
        public T GetUIPrefab<T>(string uiPrefabGuid) where T : MonoBehaviour
        {
            return _loadedPrefabs[uiPrefabGuid].GetComponent<T>();
        }

        public void Dispose()
        {
            foreach (var loadedPrefab in _loadedPrefabs.Values)
                Addressables.Release(loadedPrefab);
        }
    }
}