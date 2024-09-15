using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    private int _minCubesCount = 1;
    private int _maxCubesCount = 7;
    private int _spawnChanceDevider = 2;

    public event Action<Cube> ObjectSpawned;
    public event Action<Cube> SpawnFailed;

    private void Awake()
    {
        _inputHandler.CubeFounded += TrySpawnCubes;
    }

    private void TrySpawnCubes(Cube cube)
    {
        if (UnityEngine.Random.value <= cube.GetSpawnChance())
            SpawnCubes(cube);
        else
            SpawnFailed?.Invoke(cube);
    }

    private void SpawnCubes(Cube cube)
    {
        var cubeCount = UnityEngine.Random.Range(_minCubesCount, _maxCubesCount + 1);

        for (int i = 0; i < cubeCount; i++)
        {
            var newCube = Instantiate(cube, GetNewSpawnPosition(cube), Quaternion.identity);
            ObjectSpawned?.Invoke(newCube.GetComponent<Cube>());
            newCube.ReduceSpawnChance(_spawnChanceDevider);
        }
    }

    private List<Transform> GetAllSpawnPointsTransforms(Cube cube)
    {
        List<Transform> spawnPoints = new List<Transform>();

        for (int i = 0; i < cube.transform.childCount; i++)
        {
            spawnPoints.Add(cube.transform.GetChild(i).transform);
        }

        return spawnPoints;
    }

    private Vector3 GetNewSpawnPosition(Cube cube)
    {
        var spawnPoints = GetAllSpawnPointsTransforms(cube);
        var newPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        Destroy(newPosition.gameObject);
        return newPosition.position;
    }

    private void OnDisable()
    {
        _inputHandler.CubeFounded -= TrySpawnCubes;
    }
}
