using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Creature _target;

    public Creature Target => _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Creature enemy))
            _target = enemy;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Creature enemy))
            _target = null;
    }

}
