using UnityEngine;

namespace ShootEmUp.StartScreen
{
    [CreateAssetMenu(fileName = "Start Screen Config", menuName = "Configs/Start Screen Config")]
    public class StartScreenConfig : ScriptableObject
    {
        [SerializeField] private int _delayBeforeStart = 3;

        public int DelayBeforeStart => _delayBeforeStart;
    }
}