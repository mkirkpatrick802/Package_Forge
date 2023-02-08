using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using Random = System.Random;

public class SceneLoader : MonoBehaviour
{
    public static event Action<int> NewSceneLoading;

    private int _lowerSceneBound = 2;
    private int _upperSceneBound = 4;

    private static Random _rng = new Random();
    private List<int> _sceneQueue = new List<int>();
    private int _currentSceneInQueue = -1;

    private void Start()
    {
        for(int i = _lowerSceneBound; i <= _upperSceneBound; i++)
        {
            _sceneQueue.Add(i);
        }

        Shuffle<int>(_sceneQueue);
    }

    public void NextRandomScene()
    {
        _currentSceneInQueue++;
        int sceneToBeLoaded = _sceneQueue[_currentSceneInQueue];
        NewSceneLoading?.Invoke(sceneToBeLoaded);
        SceneManager.LoadScene(sceneToBeLoaded);

        if (_currentSceneInQueue < _sceneQueue.Count - 1) return;
        Shuffle<int>(_sceneQueue);
        _currentSceneInQueue = -1;
    }

    public void NextScene()
    {
        int sceneToBeLoaded = SceneManager.GetActiveScene().buildIndex + 1;
        NewSceneLoading?.Invoke(sceneToBeLoaded);
        SceneManager.LoadScene(sceneToBeLoaded);
    }

    public void EndGame()
    {
        NewSceneLoading?.Invoke(0);
        SceneManager.LoadScene(0);
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