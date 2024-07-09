using System;
using UnityEngine;

namespace ShootEmUp.Common
{
    public interface IGameObjectSpawner
    {
        event Action<GameObject> SpawnedObject;
        event Action<GameObject> ReleasedObject;
    }
}