using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _target;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _spawnRate;

    private ObjectPool<Enemy> _pool;
    private Coroutine _coroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(createFunc: () => Instantiate(_enemy),
            actionOnGet: (enemy) => ResetObjectParameters(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(StartSpawning());
    }

    private void ResetObjectParameters(Enemy enemy)
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

    private IEnumerator StartSpawning()
    {
        var wait = new WaitForSeconds(_spawnRate);

        while (true)
        {
            GetObject();
            yield return wait;
        }
    }
}
