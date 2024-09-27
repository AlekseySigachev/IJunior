using UnityEngine;

public class Creature : MonoBehaviour
{
    private static readonly int IsRunningAnimKey = Animator.StringToHash("IsRunning");

    [SerializeField] private float _speed;

    protected Rigidbody2D Rigidbody;
    protected Animator _animator;
    protected Vector2 CurrentDirection;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        var xVelocity = CurrentDirection.x * _speed;
        Rigidbody.velocity = new Vector2(xVelocity, Rigidbody.velocity.y);
        _animator.SetBool(IsRunningAnimKey, xVelocity != 0);
        UpdateSpriteDirection(CurrentDirection);
    }

    public void SetDirection(Vector2 direction) =>
        CurrentDirection = direction;

    private void UpdateSpriteDirection(Vector2 direction)
    {
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}