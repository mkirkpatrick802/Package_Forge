using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector2> MousePosition;
    public event Action<bool[]> KeysPressed;

    private int[] _values;
    private bool[] _keys;

    void Awake()
    {
        _values = (int[])System.Enum.GetValues(typeof(KeyCode));
        _keys = new bool[_values.Length];
    }

    void Update()
    {
        for (int i = 0; i < _values.Length; i++)
        {
            _keys[i] = Input.GetKey((KeyCode)_values[i]);
            //if (_keys[i]) print(i);
            KeysPressed?.Invoke(_keys);
        }
    }
}
