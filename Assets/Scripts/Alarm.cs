using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AlarmTrigger _trigger;

    private Coroutine _coroutine;
    private float _minValue = 0.0f;
    private float _maxValue = 1.0f;
    private float _delay = 1.0f;
    private float _volumeStep = 0.2f;

    private void OnEnable()
    {
        _trigger.ThiefEntered += StartSignal;
        _trigger.ThiefExit += EndSignal;
    }

    private void OnDisable()
    {
        _trigger.ThiefEntered -= StartSignal;
        _trigger.ThiefExit -= EndSignal;
    }

    private void StartSignal()
    {
        _coroutine = StartCoroutine(AlarmTriggered(_maxValue));
    }

    private void EndSignal()
    {
        StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(AlarmTriggered(_minValue));
    }

    private void ChangeVolume(float volume) => _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _volumeStep);

    private IEnumerator AlarmTriggered(float volume)
    {
        var wait = new WaitForSeconds(_delay);
        _audioSource.Play();

        while (_audioSource.volume != volume)
        {
            ChangeVolume(volume);
            
            if(_audioSource.volume == _minValue)
                _audioSource.Stop();

            yield return wait;
        }
    }
}
