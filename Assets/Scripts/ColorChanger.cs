using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private const string NewColor = "_Color";

    private float _alpha = 1.0f;

    private void Awake()
    {
        _spawner.ObjectSpawned += SetRandomColor;
    }
    private void OnDisable()
    {
        _spawner.ObjectSpawned -= SetRandomColor;
    }

    public void SetRandomColor(Cube cube)
    {
        Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), _alpha);
        cube.GetComponent<MeshRenderer>().material.SetColor(NewColor, newColor);
    }
}
