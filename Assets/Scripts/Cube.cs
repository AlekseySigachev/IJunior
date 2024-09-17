using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isFell;

    public event Action<GameObject> CubeFell;

    private void OnEnable()
    {
        _isFell = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null && _isFell == false)
        {
            CubeFell?.Invoke(gameObject);
            _isFell = true;
        }
    }
}
