using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    private float _delay = 0.5f;
    private int _count = 0;
    private bool _isRunning = false;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _inputHandler.MousePressed += _inputHandler_MousePressed;
    }

    private void OnDisable()
    {
        _inputHandler.MousePressed -= _inputHandler_MousePressed;
    }

    private void _inputHandler_MousePressed()
    {
        if (_isRunning)
            Stop();
        else
            Restart();
    }

    private void Restart()
    {
        _coroutine = StartCoroutine(StartAddNumbers(_delay));
        _isRunning = true;
    }

    private void Stop()
    {
        StopCoroutine(_coroutine);
        _isRunning = false;
    }

    private IEnumerator StartAddNumbers(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _count++;
            Debug.Log(_count);
            yield return wait;
        }
    }
}
