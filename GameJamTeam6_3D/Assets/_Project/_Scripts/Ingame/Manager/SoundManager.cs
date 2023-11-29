using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project._Scripts.Ingame.Manager
{
    public enum Sound
    {
        BGM,
        SFX
    }

    public class SoundManager : SerializedMonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private Dictionary<Sound, AudioClip> musicSounds, sfxSounds;
        [SerializeField] private AudioSource musicSource, sfxSource;

        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
                Instance = this;
        }
        private void Start() { PlayMusicSmoothly(Sound.BGM, 2); }

        public void PlayMusic(Sound sound)
        {
            if (musicSounds.ContainsKey(sound) == false)
            {
                ColorDebug.DebugRed("Does not contain " + sound + " key");
                return;
            }

            musicSource.clip = musicSounds[sound];
            musicSource.Play();
        }

        public void PlaySFX(Sound sound)
        {
            if (sfxSounds.ContainsKey(sound) == false)
            {
                ColorDebug.DebugRed("Does not contain " + sound + " key");
                return;
            }

            sfxSource.clip = sfxSounds[sound];
            sfxSource.Play();
        }

        private IEnumerator IEWaitForSound(float seconds, float duration)
        {
            yield return new WaitForSeconds(seconds);
            musicSource.DOFade(0, duration).onComplete += () =>
            {
                musicSource.Play();
                musicSource.DOFade(1, duration);
                StartCoroutine(IEWaitForSound(musicSource.clip.length - duration, duration));
            };
        }

        private void PlayMusicSmoothly(Sound sound, float duration)
        {
            if (musicSounds.ContainsKey(sound) == false)
            {
                ColorDebug.DebugRed("Does not contain " + sound + " key");
                return;
            }

            musicSource.clip = musicSounds[sound];
            musicSource.Play();
            musicSource.DOFade(1, duration);
            StartCoroutine(IEWaitForSound(musicSource.clip.length - duration, duration));
        }

        public void ToggleMusic() { musicSource.mute = !musicSource.mute; }

        public void ToggleSFX() { sfxSource.mute = !sfxSource.mute; }

        public void SetMusicVolume(float volume) { musicSource.volume = volume; }

        public void SetSFXVolume(float volume) { sfxSource.volume = volume; }
    }
}