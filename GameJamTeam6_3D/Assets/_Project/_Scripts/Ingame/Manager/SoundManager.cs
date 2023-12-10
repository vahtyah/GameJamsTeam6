using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project._Scripts.Ingame.Manager
{
    public enum Sound
    {
        BGM,
        Pickup,
        Shot,
        HitEnemy
    }

    public class SoundManager : SerializedMonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioMixerGroup sfxMixerGroup, musicMixerGroup;
        [SerializeField] private Dictionary<Sound, AudioClip> musicSounds, sfxSounds;
        private AudioSource musicSource;

        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            musicSource = AudioSourcePool.instance.Activate();
            musicSource.outputAudioMixerGroup = musicMixerGroup;
            PlayMusicSmoothly(Sound.BGM, 2); 
        }

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

            AudioSource audioSource = AudioSourcePool.instance.Activate();
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
            audioSource.PlayOneShot(sfxSounds[sound]);
            StartCoroutine(IEWaitForDeactivate(sfxSounds[sound].length, audioSource));
        }

        private IEnumerator IEWaitForDeactivate(float seconds, AudioSource audioSource)
        {
            yield return new WaitForSeconds(seconds);
            AudioSourcePool.instance.Deactivate(audioSource);
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
    }
}