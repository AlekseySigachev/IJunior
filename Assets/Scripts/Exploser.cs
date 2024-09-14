using UnityEngine;

public class Exploser : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
    }
    private void OnDestroy()
    {
        Explode();
    }

    private void Explode()
    {
        foreach (var cube in _spawner.CreatedObjects)
            if(cube.TryGetComponent(out Rigidbody cubeRigidbody))
               cubeRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
