using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private float _delay = 0.5f;
    private int _count = 0;
    private bool _isRunning = false;
    private Coroutine _coroutine;

    private void Update()
    {
        if (_isRunning && Input.GetMouseButtonDown(0))
        {
            Stop();
            return;
        }

        if(_isRunning == false && Input.GetMouseButtonDown(0))
        {
            Restart();
        }
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
