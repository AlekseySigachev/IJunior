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

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(createFunc: () => Instantiate(_enemy),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), _spawnDelay, _spawnRate);
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = GetRandomSpawnPoint().position;
        enemy.SetDirection(_moveDirection);
        enemy.gameObject.SetActive(true);
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
