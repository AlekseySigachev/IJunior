using System;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    public event Action<Character> Detected;
    public event Action Lost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character target))
            Detected?.Invoke(target);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character target))
            Lost?.Invoke();
    }
}
