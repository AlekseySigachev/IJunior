using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector3 _moveDirection;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(createFunc: () => Instantiate(_enemy.gameObject),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), _spawnDelay, _spawnRate);
    }

    private void ActionOnGet(GameObject gameObject)
    {
        gameObject.transform.position = GetRandomSpawnPoint().position;
        gameObject.GetComponent<Enemy>().SetDirection(_moveDirection);
        gameObject.SetActive(true);
    }

    private void GetObject()
    {
        if (_pool != null)
            _pool.Get();
    }

    private Transform GetRandomSpawnPoint()
    {
        var minRandomValue = 0;
        var maxRandomValue = _spawnPoints.Count;

        return _spawnPoints[UnityEngine.Random.Range(minRandomValue, maxRandomValue)];
    }
}
