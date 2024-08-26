using System.Collections.Generic;
using ShootEmUp.GameStates;
using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp.Common
{
    public abstract class MonoObjectsPool<T> : MonoOjbectsFabric, IGameInitializeListener where T : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _objectsContainer;
        [SerializeField] private Transform _defaultWorldTransform;

        private readonly Queue<T> _currentPool = new();

        public override event UnityAction<GameObject> Created;

        void IGameInitializeListener.OnInitialize()
        {
            for (int i = 0; i < _initialCount; i++)
            {
                T obj = Instantiate(_prefab, _objectsContainer);
                _currentPool.Enqueue(obj);
                Created?.Invoke(obj.gameObject);
            }
        }

        public T Get()
        {
            T obj;

            if (_currentPool.TryDequeue(out obj))
                obj.transform.SetParent(_defaultWorldTransform);
            else
                obj = Instantiate(_prefab, _defaultWorldTransform);

            return obj;
        }

        public void Release(T obj)
        {
            obj.transform.SetParent(_objectsContainer);
            _currentPool.Enqueue(obj);
        }
    }
}