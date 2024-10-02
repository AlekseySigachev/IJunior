using System.Collections;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private CharacterDetector _detector;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private int _attackCooldown;
    [SerializeField] private float _chasingSpeed;

    private float _defaultSpeed;
    private Coroutine _coroutine;

    protected override void Awake()
    {
        base.Awake();
        _defaultSpeed = Speed;
    }

    private void OnEnable()
    {
        _detector.Detected += OnCharacterDetected;
        _detector.Lost += OnCharacterLost;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnCharacterDetected;
        _detector.Lost -= OnCharacterLost;
    }

    private void OnCharacterLost()
    {
        Speed = _defaultSpeed;
        _patrol.StartDoPatrol();
        StopCoroutine(_coroutine);
    }

    private void OnCharacterDetected(Character character)
    {
        Speed = _chasingSpeed;
        _patrol.StopDoPatrol();
        _coroutine = StartCoroutine(StartAttacking());
    }

    private IEnumerator StartAttacking()
    {
        var wait = new WaitForSeconds(_attackCooldown);

        while (enabled)
        {
            if (AttackArea.Target != null)
            {
                StartAttackAnimation();
                yield return wait;
            }

            yield return null;
        }
    }
}
