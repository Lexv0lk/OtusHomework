using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.Managers
{
    public sealed class GameFinishObserver : MonoBehaviour, IGameFinishListener
    {
        void IGameFinishListener.OnFinish()
        {
            Debug.Log("Game over!");
        }
    }
}