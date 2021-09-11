using System.Collections.Generic;
using System.Linq;
using DAATS.Initializer.Mangers.Sound.Interface;
using UnityEngine;

namespace DAATS.Initializer.Mangers.Sound
{
    public class SoundManager : MonoBehaviour, ISoundManager
    {
        [SerializeField]
        private AudioSource _musicAudioSource;
        [SerializeField]
        private AudioSource _soundAudioSource;

        [SerializeField]
        private List<AudioClip> _musicAudioClips;
        [SerializeField]
        private List<AudioClip> _soundAudioClips;

        public void PlayMusic(string name)
        {
            var music = FindMusicWithName(name);
            if (music == null || _musicAudioSource.clip == music) return;
            StopMusic();
            _musicAudioSource.clip = music;
            _musicAudioSource.Play();
        }

        public void PlaySound(string name)
        {
            var sound = FindSoundWithName(name);
            if (sound == null || _soundAudioSource.clip == sound) return;
            StopSound();
            _soundAudioSource.clip = sound;
            _soundAudioSource.Play();
        }

        public void StopMusic()
        {
            _musicAudioSource.Stop();
        }

        public void StopSound()
        {
            _soundAudioSource.Stop();
        }

        public void SetMusicVolume(float volume)
        {
            _musicAudioSource.volume = volume;
        }

        public void SetSoundVolume(float volume)
        {
            _soundAudioSource.volume = volume;
        }

        public void EnableMusic(bool enable)
        {
            _musicAudioSource.mute = !enable;
        }

        public void EnableSound(bool enable)
        {
            _soundAudioSource.mute = !enable;
        }

        private AudioClip FindMusicWithName(string name)
        {
            return _musicAudioClips.FirstOrDefault(clip => clip.name == name);
        }

        private AudioClip FindSoundWithName(string name)
        {
            return _soundAudioClips.FirstOrDefault(clip => clip.name == name);
        }
    }
}