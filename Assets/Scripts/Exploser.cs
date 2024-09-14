using System.Collections.Generic;
using UnityEngine;

public class Exploser : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Spawner _spawner;

    public void Explode()
    {
        foreach (var cube in GetExploadableObjects())
            cube.AddExplosionForce(_explosionForce / transform.localScale.x, transform.position, _explosionRadius / transform.localScale.x);
    }

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
    }

    private List<Rigidbody> GetExploadableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> objectsToExplode = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objectsToExplode.Add(hit.attachedRigidbody);

        return objectsToExplode;
    }
}
