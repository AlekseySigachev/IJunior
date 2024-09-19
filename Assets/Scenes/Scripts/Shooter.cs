using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Shooter : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _prefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform _target;

    private void Start() => 
        StartCoroutine(StartShooting());

    private Vector3 GetDirection() => 
        (_target.position - transform.position).normalized;

    private MonoBehaviour SpawnBullet() => 
        Instantiate(_prefab, transform.position + GetDirection(), Quaternion.identity);

    private IEnumerator StartShooting()
    {
        var wait = new WaitForSeconds(_shootDelay);

        while (true)
        {
            var newBullet = SpawnBullet();
            var direction = GetDirection();
            var bulletRigidbody = newBullet.GetComponent<Rigidbody>();

            bulletRigidbody.transform.up = direction;
            bulletRigidbody.velocity = direction * _speed;

            yield return wait;
        }
    }
}