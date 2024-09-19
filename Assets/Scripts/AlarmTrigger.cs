using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            ThiefEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            ThiefExit?.Invoke();
    }
}
