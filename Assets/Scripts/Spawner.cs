using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnDelay;

    private ObjectPool<GameObject> _pool;

    public event Action<GameObject> OnSpawned;
    public event Action<GameObject> OnSpawnedCubeFell;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(createFunc: () => Instantiate(_prefab),
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
        gameObject.transform.position = GetNewSpawnPosition();
        gameObject.SetActive(true);
        gameObject.GetComponent<Cube>().CubeFell += OnCubeFell;
        OnSpawned?.Invoke(gameObject);
    }

    private void OnCubeFell (GameObject gameObject)
    {
        gameObject.GetComponent<Cube>().CubeFell -= OnCubeFell;
        StartCoroutine(TurnOffGameObjectWithDelay(gameObject));
        OnSpawnedCubeFell?.Invoke(gameObject);
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

    private IEnumerator TurnOffGameObjectWithDelay(GameObject obj)
    {
        var minRandomValue = 2;
        var maxRandomValue = 5;
        var waitTime = UnityEngine.Random.Range(minRandomValue, maxRandomValue);
        var wait = new WaitForSeconds(waitTime);

        yield return wait;
        _pool.Release(obj);
    }
}
