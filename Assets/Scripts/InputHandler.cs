using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action MousePressed;
    private int _mouseButtonNumber = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_mouseButtonNumber))
            MousePressed?.Invoke();
    }
}