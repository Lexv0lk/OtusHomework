using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Pipeline.Tasks.Visual
{
    public class AudioTask : EventTask
    {
        private readonly AudioClip _clip;
        private readonly AudioPlayer _audioPlayer;
        private readonly bool _waitForEnd;

        public AudioTask(AudioClip clip, AudioPlayer audioPlayer, bool waitForEnd = true)
        {
            _clip = clip;
            _audioPlayer = audioPlayer;
            _waitForEnd = waitForEnd;
        }

        protected override void OnRun()
        {
            PlaySound().Forget();
        }
        
        private async UniTaskVoid PlaySound()
        {
            _audioPlayer.PlaySound(_clip);

            if (_waitForEnd)
                await UniTask.Delay((int)(_clip.length * 1000));
            
            Finish();
        }
    }
}