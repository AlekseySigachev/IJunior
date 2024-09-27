using UnityEngine;

public class Diamond : MonoBehaviour
{
    private Animator _animator;

    private static readonly int IsHittingAnimKey = Animator.StringToHash("hit");

    private void Awake() =>
        _animator = GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            character.IncreaseDiamonds();
            _animator.SetTrigger(IsHittingAnimKey);
        }
    }

    private void Hit() =>
        Destroy(gameObject);
}
