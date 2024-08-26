using UnityEngine;

namespace ShootEmUp.GameStates
{
    public enum GameState
    {
        OFF = 0,
        PLAYING = 1,
        PAUSED = 2,
        FINISHED = 3
    }
    
    public class GameStateModel : MonoBehaviour
    {
        public GameState CurrentState { get; private set; }

        public void ChangeCurrentState(GameState newState)
        {
            CurrentState = newState;
        }
    }
}