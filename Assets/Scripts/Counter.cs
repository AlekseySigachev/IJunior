using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    public event Action NumberUpdated;
    private float _delay = 0.5f;
    private bool _isRunning = false;
    private Coroutine _coroutine;

    public int Count { get; private set; }

    private void OnEnable()
    {
        _inputHandler.MousePressed += ToggleCount;
    }

    private void OnDisable()
    {
        _inputHandler.MousePressed -= ToggleCount;
    }

    private void ToggleCount()
    {
        if (_isRunning)
            Stop();
        else
            Restart();
    }

    private void Restart()
    {
        _coroutine = StartCoroutine(AddNumbers(_delay));
        _isRunning = true;
    }

    private void Stop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _isRunning = false;
        }
    }

    private IEnumerator AddNumbers(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Count++;
            NumberUpdated?.Invoke();
            yield return wait;
        }
    }
}
