using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public float ChanceToSpawn = 1.0f;
    [HideInInspector] public UnityEvent<Cube> ObjectSpawned;
    [SerializeField] private GameObject _prefab;

    private InputHandler _inputHandler;
    private List<Transform> _spawnPoints = new List<Transform>();
    private int _minCubesCount = 1;
    private int _maxCubesCount = 7;
    private int _spawnChanceDevider = 2;

    private List<GameObject> _createdObjects;
    public List<GameObject> CreatedObjects = new List<GameObject>();

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _inputHandler.MousePressed.AddListener(SpawnCubes);
        _createdObjects = new List<GameObject>();
        _spawnPoints = GetAllSpawnPointsTransforms();
    }
    private List<Transform> GetAllSpawnPointsTransforms()
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
                ObjectSpawned?.Invoke(newCube.GetComponent<Cube>());
                ReduceSpawnChance(newCube);
                _createdObjects.Add(newCube);
            }

            CreatedObjects = _createdObjects;
        }
    }

    private void ReduceSpawnChance(GameObject gameObject)
    {
        gameObject.GetComponent<Spawner>().ChanceToSpawn /= _spawnChanceDevider;
    }

    private Vector3 GetNewSpawnPosition()
    {
        var newPosition = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        _spawnPoints.Remove(newPosition);
        return newPosition.position;
    }

    private void OnDisable()
    {
        _inputHandler.MousePressed.RemoveListener(SpawnCubes);
    }
}
