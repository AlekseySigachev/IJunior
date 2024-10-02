using UnityEngine;

public class Creature : MonoBehaviour
{
    private static readonly int IsRunningAnimKey = Animator.StringToHash("IsRunning");
    private static readonly int IsHitAnimKey = Animator.StringToHash("Hit");
    private static readonly int IsAttackAnimKey = Animator.StringToHash("Attack");

    [SerializeField] protected AttackArea AttackArea;
    [SerializeField] protected float Speed;
    [SerializeField] protected int Damage;

    protected Rigidbody2D Rigidbody;
    protected Animator Animator;
    protected HealthComponent Health;
    protected Vector2 CurrentDirection;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<HealthComponent>();

        Health.DamageDealed += OnDamaged;
    }

    private void OnDamaged() =>
        Animator.SetTrigger(IsHitAnimKey);

    protected virtual void FixedUpdate()
    {
        var xVelocity = CurrentDirection.x * Speed;
        Rigidbody.velocity = new Vector2(xVelocity, Rigidbody.velocity.y);
        Animator.SetBool(IsRunningAnimKey, xVelocity != 0);
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

    public void StartAttackAnimation() =>
        Animator.SetTrigger(IsAttackAnimKey);

    private void OnAttack()
    {
        if (AttackArea.Target != null)
            AttackArea.Target.GetComponent<HealthComponent>().ModifyHealth(-Damage);
    }
}