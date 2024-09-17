using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Color _defaultColor;

    private void OnEnable()
    {
        _spawner.SpawnedCubeFell += SetRandomColor;
        _spawner.CubeSpawned += SetDefaulColor;
    }

    private void SetDefaulColor(Cube cube)
    {
        GetObjectMaterial(cube).color = _defaultColor;
    }

    private Material GetObjectMaterial(Cube cube) => cube.GetComponent<MeshRenderer>().material;

    private void SetRandomColor(Cube cube)
    {
        var randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GetObjectMaterial(cube).color = randomColor;
    }

    private void OnDisable()
    {
        _spawner.SpawnedCubeFell -= SetRandomColor;
        _spawner.CubeSpawned -= SetDefaulColor;
    }
}
