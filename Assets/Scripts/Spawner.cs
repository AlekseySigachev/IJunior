using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _target;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnDelay;

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
        enemy.transform.position = transform.position;
        enemy.SetTarget(_target);
        enemy.gameObject.SetActive(true);
    }

    private void GetObject()
    {
        if (_pool != null && _pool.CountActive <= _defaultCapacity -1)
            _pool.Get();
    }
}
