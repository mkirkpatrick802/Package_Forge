using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager i { private set; get; }

    public SceneLoader SceneLoader => _sceneLoader;
    public GameManager GameManager => _gameManager;
    public AudioManager AudioManager => _audioManager;
    public InputTranslator InputTranslator => _inputTranslator;

    private SceneLoader _sceneLoader;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private InputTranslator _inputTranslator;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        _sceneLoader = gameObject.transform.parent.GetComponentInChildren<SceneLoader>();
        _gameManager = gameObject.transform.parent.GetComponentInChildren<GameManager>();
        _audioManager = gameObject.transform.parent.GetComponentInChildren<AudioManager>();
        _inputTranslator = gameObject.transform.parent.GetComponentInChildren<InputTranslator>();
    }
}
