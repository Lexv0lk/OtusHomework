using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ShootEmUp.StartScreen.UI
{
    public class StartTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeLeft;

        public async UniTask AnimateTimeAsync(int seconds)
        {
            _timeLeft.text = seconds.ToString();
            int delay = 1;

            while (seconds > 0)
            {
                await UniTask.WaitForSeconds(delay);
                seconds -= delay;
                _timeLeft.text = seconds.ToString();
            }
        }
    }
}