using System.Collections.Generic;
using UnityEngine;

public class Exploser : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
        _spawner.SpawnFailed += Explode;
    }

    public void Explode(Cube cube)
    {
        foreach (var objectToExplode in GetExploadableObjects(cube))
            objectToExplode.AddExplosionForce(_explosionForce / cube.transform.localScale.x, cube.transform.position, _explosionRadius / cube.transform.localScale.x);
    }

    private List<Rigidbody> GetExploadableObjects(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, _explosionRadius);

        List<Rigidbody> objectsToExplode = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objectsToExplode.Add(hit.attachedRigidbody);

        return objectsToExplode;
    }
    private void OnDisable()
    {
        _spawner.SpawnFailed -= Explode;
    }
}
