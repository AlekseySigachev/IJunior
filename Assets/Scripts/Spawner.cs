using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(createFunc: () => Instantiate(_prefab, transform),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 15,
            maxSize: 10000);

    }

    private void ActionOnGet(GameObject gameObject) 
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, 1.0f);
    }

    private void GetObject()
    {
        if (_pool != null)
            _pool.Get();
    }
}
