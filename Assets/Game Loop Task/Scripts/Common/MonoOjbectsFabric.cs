using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp.Common
{
    public abstract class MonoOjbectsFabric : MonoBehaviour
    {
        public abstract event UnityAction<GameObject> Created;
    }
}