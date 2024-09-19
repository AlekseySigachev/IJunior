using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _minValue = 0.0f;
    private float _maxValue = 1.0f;
    private float _delay = 1.0f;
    private float _volumeStep = 0.2f;
    private bool _isThiefInHouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            _isThiefInHouse = true;
            StartSignal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            _isThiefInHouse = false;
            EndSignal();
        }
    }

    private void StartSignal()
    {
        StopAllCoroutines();
        StartCoroutine(AlarmTriggered());
    }

    private void EndSignal()
    {
        StopAllCoroutines();
        StartCoroutine(AlarmTriggered());
    }

    private void ChangeVolume(float volume) => _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _volumeStep);

    private IEnumerator AlarmTriggered()
    {
        var wait = new WaitForSeconds(_delay);

        if (_isThiefInHouse)
        {
            _audioSource.Play();

            while (_audioSource.volume != _maxValue)
            {
                ChangeVolume(_maxValue);
                yield return wait;
            }
        }
        else
        {
            while (_audioSource.volume != _minValue)
            {
                ChangeVolume(_minValue);
                yield return wait;
            }

            _audioSource.Stop();
        }
    }
}
