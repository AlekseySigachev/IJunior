using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private const string NewColor = "_Color";

    private float alpha = 1.0f;

    public void SetRandomColor(GameObject gameObject)
    {
        Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), alpha);
        gameObject.GetComponent<MeshRenderer>().material.SetColor(NewColor, newColor);
    }
}
