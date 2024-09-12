using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action MousePressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MousePressed?.Invoke();
    }
}