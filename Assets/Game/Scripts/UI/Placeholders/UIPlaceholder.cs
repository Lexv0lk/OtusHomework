using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public abstract class UIPlaceholder<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private AssetReference _uiPrefab;
        
        private UIManager _uiManager;
        private DiContainer _container;
        
        public T UI { get; private set; }
        public bool IsReady { get; private set; }

        public event Action<T> Spawned;
        
        [Inject]
        private void Construct(UIManager uiManager, DiContainer container)
        {
            _uiManager = uiManager;
            _container = container;
        }
        
        private void Awake()
        {
            UI = _container.InstantiatePrefabForComponent<T>(_uiManager.GetUIPrefab<T>(_uiPrefab.AssetGUID), transform);
            IsReady = true;
            Spawned?.Invoke(UI);
        }
    }
}