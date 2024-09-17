using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Color _defaultColor;

    private void OnEnable()
    {
        _spawner.OnSpawnedCubeFell += SetRandomColor;
        _spawner.OnSpawned += SetDefaulColor;
    }

    private void SetDefaulColor(GameObject obj)
    {
        GetObjectMaterial(obj).color = _defaultColor;
    }

    private Material GetObjectMaterial(GameObject gameObject) => gameObject.GetComponent<Material>();

    private void SetRandomColor(GameObject obj)
    {
        var randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GetObjectMaterial(obj).color = randomColor;
    }

    private void OnDisable()
    {
        _spawner.OnSpawnedCubeFell -= SetRandomColor;
    }
}
