using System.Collections.Generic;
using UnityEngine;

public class CubeExploser : MonoBehaviour
{
    [HideInInspector] public float ChanceToSpawn = 1.0f;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private GameObject _prefab;

    private int _minCubesCount = 1;
    private int _maxCubesCount = 6;
    private int _spawnChanceDevider = 2;
    private int _sizeDevider = 2;
    private List<GameObject> _createdCubes = new List<GameObject>();
    private List<Transform> _spawnPoints = new List<Transform>();
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _spawnPoints = GetAllSpawnPointsTransforns();
        SetRandomColor();
    }

    private List<Transform> GetAllSpawnPointsTransforns()
    {
        List<Transform> spawnPoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i).transform);
        }

        return spawnPoints;
    }
    private void SpawnCubes()
    {
        if (Random.value <= ChanceToSpawn)
        {
            var cubeCount = Random.Range(_minCubesCount, _maxCubesCount + 1);

            for (int i = 0; i < cubeCount; i++)
            {
                var newCube = Instantiate(_prefab, GetNewSpawnPosition(), Quaternion.identity);
                _createdCubes.Add(newCube);
                ReduceCubeSize(newCube);
                ReduceSpawnChance(newCube);
            }
        }
    }

    private Vector3 GetNewSpawnPosition()
    {
        var newPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        _spawnPoints.Remove(newPosition);
        return newPosition.position;
    }

    private void ReduceCubeSize(GameObject cube)
    {
        cube.transform.localScale = transform.localScale / _sizeDevider;
    }

    private void ReduceSpawnChance(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out CubeExploser cube))
            cube.ChanceToSpawn /= _spawnChanceDevider;
    }

    private void Explode()
    {
        foreach (var cube in _createdCubes)
            if(cube.TryGetComponent(out Rigidbody cubeRigidbody))
                cubeRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private void SetRandomColor()
    {
        Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 255.0f);
        _renderer.material.SetColor("_Color", newColor);
    }

    private void OnMouseDown()
    {
        SpawnCubes();
        Explode();
        Destroy(gameObject);
    }
}
