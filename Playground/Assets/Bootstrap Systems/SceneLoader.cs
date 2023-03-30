using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class SceneLoader : MonoBehaviour
{
    public static event Action<int> NewSceneLoading;

    [Header("Scene Indexes")]
    [SerializeField] private int mainMenuScene = 0;
    [SerializeField] private int endScreenScene;

    [Header("Scene Queue System")]
    [SerializeField] private int lowerSceneBound = 2;
    [SerializeField] private int upperSceneBound = 4;

    [Header("Scene Transition")]
    [SerializeField] private float animationTime = 1;
    [SerializeField] private AnimationClip startClip;
    [SerializeField] private AnimationClip endClip;

    private static Random _rng = new Random();
    private List<int> _sceneQueue = new List<int>();
    private int _currentSceneInQueue = -1;
    private Animator _transition;
    private bool _isInit = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= NewSceneLoaded;
    }

    private void Start()
    {
        for (int i = lowerSceneBound; i <= upperSceneBound; i++)
        {
            _sceneQueue.Add(i);
        }

        Shuffle<int>(_sceneQueue);
        _transition = GetComponentInChildren<Animator>();

        _isInit = true;
    }

    public void NextRandomScene()
    {
        _currentSceneInQueue++;
        int sceneToBeLoaded = _sceneQueue[_currentSceneInQueue];
        StartCoroutine(LoadSceneFromIndex(sceneToBeLoaded));

        if (_currentSceneInQueue < _sceneQueue.Count - 1) return;
        Shuffle<int>(_sceneQueue);
        _currentSceneInQueue = -1;
    }

    public void NextScene()
    {
        int sceneToBeLoaded = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneToBeLoaded == endScreenScene)
            StartCoroutine(LoadSceneFromIndex(mainMenuScene));
        else
            StartCoroutine(LoadSceneFromIndex(sceneToBeLoaded));
    }

    public void RestartScene()
    {
        int sceneToBeLoaded = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneFromIndex(sceneToBeLoaded));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneFromIndex(mainMenuScene));
    }

    public void LoadEndScreen()
    {
        StartCoroutine(LoadSceneFromIndex(endScreenScene));
    }

    private IEnumerator LoadSceneFromIndex(int index)
    {
        _transition.Play(startClip.name);

        yield return new WaitForSeconds(animationTime);

        NewSceneLoading?.Invoke(index);
        SceneManager.LoadScene(index);
    }

    private void NewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!_isInit) return;

        _transition.Play(endClip.name);
    }

    private static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}