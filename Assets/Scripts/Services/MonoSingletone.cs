using UnityEngine;

namespace Client.Services
{
    public abstract class MonoSingletone<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}