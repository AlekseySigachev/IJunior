using UnityEngine;

public class Character : Creature
{
    [SerializeField] private LayerChecker _groundCheck;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private bool _isJumping;
    private int _diamonds;

    private static readonly int IsGroundedAnimKey = Animator.StringToHash("IsGrounded");
    private static readonly int YVelocityAnimKey = Animator.StringToHash("VerticalVelocity");

    private void Update()
    {
        _isGrounded = _groundCheck.IsTouchingLayer;
        _animator.SetBool(IsGroundedAnimKey, _isGrounded);
    }

    protected override void FixedUpdate()
    {
        var isJumpPressing = CurrentDirection.y > 0;

        if (isJumpPressing && _isGrounded)
            Rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        _animator.SetFloat(YVelocityAnimKey, Rigidbody.velocity.y);
        base.FixedUpdate();
    }

    public void IncreaseDiamonds() => _diamonds++;
}
