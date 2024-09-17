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

    private void SetDefaulColor(GameObject obj)
    {
        GetObjectMaterial(obj).color = _defaultColor;
    }

    private Material GetObjectMaterial(GameObject gameObject) => gameObject.GetComponent<MeshRenderer>().material;

    private void SetRandomColor(GameObject obj)
    {
        var randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GetObjectMaterial(obj).color = randomColor;
    }

    private void OnDisable()
    {
        _spawner.SpawnedCubeFell -= SetRandomColor;
        _spawner.CubeSpawned -= SetDefaulColor;
    }
}
