using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private int _sizeDevider = 2;

    private void Awake()
    {
        _spawner.ObjectSpawned += ReduceObjectSize;
    }

    public void ReduceObjectSize(Cube cube)
    {
        cube.transform.localScale = cube.transform.localScale / _sizeDevider;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= ReduceObjectSize;
    }
}
