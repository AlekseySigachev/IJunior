using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public  event Action<Cube> ObjectSpawned;
    public event Action<Cube> SpawnFailed;

    private InputHandler _inputHandler;
    private Exploser _exploser;
    private int _minCubesCount = 1;
    private int _maxCubesCount = 7;
    private int _spawnChanceDevider = 2;

    private void Awake()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
        _exploser = GetComponent<Exploser>();
        _inputHandler.CubeFounded += SpawnCubes;
    }

    public void SpawnCubes(Cube cube)
    {
        if (UnityEngine.Random.value <= cube.GetSpawnChance())
        {
            var cubeCount = UnityEngine.Random.Range(_minCubesCount, _maxCubesCount + 1);

            for (int i = 0; i < cubeCount; i++)
            {
                var newCube = Instantiate(cube, GetNewSpawnPosition(cube), Quaternion.identity);
                ObjectSpawned?.Invoke(newCube.GetComponent<Cube>());
                newCube.ReduceSpawnChance(_spawnChanceDevider);
            }
        }
        else
        {
            SpawnFailed?.Invoke(cube);
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
        _inputHandler.CubeFounded -= SpawnCubes;
    }
}
