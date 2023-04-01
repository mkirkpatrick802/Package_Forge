using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTranslator : MonoBehaviour
{

    private enum Keys
    { 
        Escape = 6,
    };

    public event Action<Vector2> MouseMoved;
    public event Action QuitGame;

    private InputManager _inputManager;

    private Vector2 _lastMousePos;

    private void Awake()
    {
        _inputManager = gameObject.transform.parent.GetComponentInChildren<InputManager>();
    }

    private void OnEnable()
    {
        _inputManager.MousePosition += MousePosition;
        _inputManager.KeysPressed += KeysPressed;
    }

    private void OnDisable()
    {
        _inputManager.MousePosition -= MousePosition;
        _inputManager.KeysPressed -= KeysPressed;
    }

    private void KeysPressed(bool[] keys)
    {
        if (keys[(int)Keys.Escape])
        {
            QuitGame?.Invoke();
        }
    }

    private void MousePosition(Vector2 pos)
    {
        if (_lastMousePos == pos) return;
        _lastMousePos = pos;

        MouseMoved?.Invoke(pos);
    }
}
