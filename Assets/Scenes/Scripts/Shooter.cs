using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootDelay;

    private Transform _target;
    private Coroutine _coroutine;

    private void Start() => _coroutine = StartCoroutine(StartShooting());

    public void SetTarget(Transform target) => _target = target;

    private Vector3 GetDirection() => (_target.position - transform.position).normalized;

    private GameObject SpawnBullet() => Instantiate(_prefab, transform.position + GetDirection(), Quaternion.identity);

    IEnumerator StartShooting()
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