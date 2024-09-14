using UnityEngine;

public class Cube : MonoBehaviour
{
    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _inputHandler.MousePressed.AddListener(Destroy);
    }
    public void Destroy()
    {
        _inputHandler.MousePressed.RemoveListener(Destroy);
        Destroy(gameObject);
    }
}
