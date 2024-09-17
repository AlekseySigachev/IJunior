using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isfell;

    public event Action<GameObject> CubeFell;

    private void OnEnable()
    {
        _isfell = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null && _isfell == false)
        {
            CubeFell?.Invoke(gameObject);
            _isfell = true;
        }
    }
}
