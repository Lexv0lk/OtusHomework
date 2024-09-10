using UnityEngine;

namespace Entities.Components
{
    public struct SoundsComponent
    {
        public AudioClip[] AbilityClips;
        public AudioClip[] DeathClips;
        public AudioClip[] LowHealthClips;
        public AudioClip[] StartTurnClips;
        
        public SoundsComponent(AudioClip[] abilityClips, AudioClip[] deathClips, AudioClip[] lowHealthClips, AudioClip[] startTurnClips)
        {
            AbilityClips = abilityClips;
            DeathClips = deathClips;
            LowHealthClips = lowHealthClips;
            StartTurnClips = startTurnClips;
        }

        public AudioClip GetRandomAbilityClip() => GetRandomClip(AbilityClips);
        public AudioClip GetRandomDeathClip() => GetRandomClip(DeathClips);
        public AudioClip GetRandomLowHealthClip() => GetRandomClip(LowHealthClips);
        public AudioClip GetRandomStartTurnClip() => GetRandomClip(StartTurnClips);

        private AudioClip GetRandomClip(AudioClip[] clips) => clips[Random.Range(0, clips.Length)];
    }
}