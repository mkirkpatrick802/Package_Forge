using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<GameStates> GameStateChanged;

    private GameStates _currentState;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        SceneLoader.NewSceneLoading += NewScene;
    }

    private void OnDisable()
    {
        SceneLoader.NewSceneLoading -= NewScene;
    }

    public void ChangeGameState(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.MainMenu:
                break;
            case GameStates.EndScreen:
                break;
        }

        print("New Game State [" + newState.ToString() + "] Active");

        _currentState = newState;
        GameStateChanged?.Invoke(_currentState);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void NewScene(int scene)
    {
        print("Scene Number [" + scene + "] Loaded!!!");
    }
}

public enum GameStates
{
    MainMenu,
    EndScreen,
}
