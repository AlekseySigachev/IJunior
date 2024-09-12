using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private void OnEnable()
    {
        _counter.NumberUpdated += NumberUpdated;
    }

    private void OnDisable()
    {
        _counter.NumberUpdated -= NumberUpdated;
    }

    private void NumberUpdated()
    {
        ShowInfo(_counter.Count);
    }

    private void ShowInfo(int count)
    {
        Debug.Log(count);
    }
}
