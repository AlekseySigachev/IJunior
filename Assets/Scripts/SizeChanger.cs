using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    private Spawner _spawner;
    private int _sizeDevider = 2;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _spawner.ObjectSpawned.AddListener(ReduceObjectSize);
    }
    public void ReduceObjectSize(Cube cube)
    {
        cube.transform.localScale = transform.localScale / _sizeDevider;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned.AddListener(ReduceObjectSize);
    }
}
