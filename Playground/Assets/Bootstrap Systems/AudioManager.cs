using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static event Action SequenceFinished;

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public bool IsNarratorPlaying()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying && s.narrator)
                return true;
        }

        return false;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) { print(name + " Audio File Not Found"); return; }
        if (s.source.isPlaying && s.noDup) return;
        else if (s.source.isPlaying)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = s.clip;

            newSource.loop = s.loop;
            newSource.volume = s.volume;
            newSource.pitch = s.pitch;

            newSource.Play();
            StartCoroutine(CleanUpAudioSource(newSource));
        }
        else
        {
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { print(name + " Audio File Not Found"); return; }
        s.source.Stop();
    }

    public void PlayAudioSequence(Sound[] soundsToPlay)
    {
        List<Sound> soundBuffer = new List<Sound>();

        foreach (Sound manager in sounds)
        {
            foreach (Sound request in soundsToPlay)
            {
                if (manager == request) soundBuffer.Add(manager);
            }
        }

        if (soundBuffer.Count != soundsToPlay.Length) Debug.LogWarning("Missing Sounds in AudioManager");

        StartCoroutine(PlaySequence(soundBuffer));
    }

    private IEnumerator PlaySequence(List<Sound> sounds)
    {
        foreach (Sound s in sounds)
        {
            Play(s.name);
            yield return new WaitForSeconds(s.clip.length);
        }

        SequenceFinished?.Invoke();
    }

    private IEnumerator CleanUpAudioSource(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source);
    }
}
