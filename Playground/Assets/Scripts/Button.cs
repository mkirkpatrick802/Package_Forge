using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = ReferenceManager.i.AudioManager;
        _audioManager.Play("Rain");
    }

    public void Clicked()
    {
        _audioManager.Play("Oof");
    }
}
