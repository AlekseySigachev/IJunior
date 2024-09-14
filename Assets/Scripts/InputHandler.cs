using System;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public UnityEvent MousePressed;

    private void OnMouseDown()
    {
        MousePressed?.Invoke();
    }
}
