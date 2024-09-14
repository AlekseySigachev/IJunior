using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public float ChanceToSpawn = 1.0f;
    [SerializeField] private GameObject _prefab;

    private List<Transform> _spawnPoints = new List<Transform>();
    private int _minCubesCount = 1;
    private int _maxCubesCount = 7;
    private int _spawnChanceDevider = 2;

    public UnityEvent<GameObject> ObjectSpawned;

    public List<GameObject> CreatedObjects { get; private set; }

    private void Awake()
    {
        CreatedObjects = new List<GameObject>();
        _spawnPoints = GetAllSpawnPointsTransforns();
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

    public void SpawnCubes()
    {
        if (Random.value <= ChanceToSpawn)
        {
            var cubeCount = Random.Range(_minCubesCount, _maxCubesCount + 1);

            for (int i = 0; i < cubeCount; i++)
            {
                var newCube = Instantiate(_prefab, GetNewSpawnPosition(), Quaternion.identity);
                ObjectSpawned?.Invoke(newCube);
                ReduseSpawnChance(newCube);
                CreatedObjects.Add(newCube);
            }
        }
    }

    private void ReduseSpawnChance(GameObject gameObject)
    {
        gameObject.GetComponent<Spawner>().ChanceToSpawn /= _spawnChanceDevider;
    }

    private Vector3 GetNewSpawnPosition()
    {
        var newPosition = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        _spawnPoints.Remove(newPosition);
        return newPosition.position;
    }
}
