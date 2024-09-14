using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private const string NewColor = "_Color";

    private Spawner _spawner;
    private float alpha = 1.0f;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _spawner.ObjectSpawned.AddListener(SetRandomColor);
    }

    public void SetRandomColor(Cube cube)
    {
        Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), alpha);
        cube.GetComponent<MeshRenderer>().material.SetColor(NewColor, newColor);
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned.AddListener(SetRandomColor);
    }
}
