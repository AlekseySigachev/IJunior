using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnDelay;

    private ObjectPool<Cube> _pool;

    public event Action<Cube> CubeSpawned;
    public event Action<Cube> SpawnedCubeFell;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), _spawnDelay, _spawnRate);
    }
    
    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetNewSpawnPosition();
        cube.gameObject.SetActive(true);
        cube.CubeFell += OnCubeFell;
        CubeSpawned?.Invoke(cube);
    }

    private void OnCubeFell (Cube cube)
    {
        cube.CubeFell -= OnCubeFell;
        StartCoroutine(TurnOffGameObjectWithDelay(cube));
        SpawnedCubeFell?.Invoke(cube);
    }

    private void GetObject()
    {
        if (_pool != null)
            _pool.Get();
    }

    private Vector3 GetNewSpawnPosition()
    {
        var newPosition = new Vector3(UnityEngine.Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), transform.position.y, UnityEngine.Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2));
        return newPosition;
    }

    private IEnumerator TurnOffGameObjectWithDelay(Cube cube)
    {
        var minRandomValue = 2;
        var maxRandomValue = 5;
        var waitTime = UnityEngine.Random.Range(minRandomValue, maxRandomValue);
        var wait = new WaitForSeconds(waitTime);

        yield return wait;
        _pool.Release(cube);
    }
}
