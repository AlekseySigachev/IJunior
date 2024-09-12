using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private void OnEnable()
    {
        _counter.NumberUpdated += ToggleView;
    }

    private void OnDisable()
    {
        _counter.NumberUpdated -= ToggleView;
    }

    private void ToggleView()
    {
        ShowInfo(_counter.Count);
    }

    private void ShowInfo(int count)
    {
        Debug.Log(count);
    }
}
