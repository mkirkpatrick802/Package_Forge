using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<GameStates> GameStateChanged;

    private InputTranslator _inputTranslator;

    private GameStates _currentState;

    private void Awake()
    {
        _inputTranslator = gameObject.transform.parent.GetComponentInChildren<InputTranslator>();
    }

    private void OnEnable()
    {
        SceneLoader.NewSceneLoading += NewScene;
        _inputTranslator.QuitGame += Quit;
    }

    private void OnDisable()
    {
        SceneLoader.NewSceneLoading -= NewScene;
        _inputTranslator.QuitGame -= Quit;
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

    private void Quit()
    {
        print("Quit");
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
