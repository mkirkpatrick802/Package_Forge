using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/New Sound", order = 1)]
public class Sound : ScriptableObject
{
    [HideInInspector]
    public AudioSource source;

    public AudioClip clip;

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool loop;
    public bool noDup;
    public bool narrator;
}
