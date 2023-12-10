using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool instance { get; private set; }
    [SerializeField] private AudioSource audioSourcePrefab;
    private readonly Queue<AudioSource> audioSources = new Queue<AudioSource>();

    private void Awake()
    {
        if(instance) Destroy(gameObject);
        else instance = this;
    }

    public AudioSource Activate()
    {
        AudioSource audioSource = audioSources.Count > 0 ? audioSources.Dequeue() : Instantiate(audioSourcePrefab, transform);
        audioSource.gameObject.SetActive(true);
        return audioSource;
    }

    public void Deactivate(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(false);
        audioSources.Enqueue(audioSource);
    }
}