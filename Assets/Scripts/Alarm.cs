using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _minValue = 0.0f;
    private float _maxValue = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            StartSignal();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            EndSignal();
    }

    private void StartSignal()
    {
        Debug.Log("Started");
        StartCoroutine(AlarmOn());
    }

    private void EndSignal()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minValue, 0.1f);
    }

    private IEnumerator AlarmOn()
    {
        while(_audioSource.volume != _maxValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxValue, 0.1f);
            Debug.Log(_audioSource.volume);
            yield return null;
        }
    }
}
