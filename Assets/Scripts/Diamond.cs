using UnityEngine;

public class Diamond : MonoBehaviour
{
    private static readonly int IsHittingAnimKey = Animator.StringToHash("hit");

    private Animator _animator;

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
