using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;

    public string name { private set; get; }
    public AudioClip clip { private set; get; }

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool loop;
    public bool noDup;
}
