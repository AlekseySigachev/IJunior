using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
[RequireComponent(typeof(Enemy))]

public class Chase : MonoBehaviour
{
    private CharacterDetector _detector;
    private Creature _owner;
    private AttackArea _attackArea;

    private void Awake()
    {
        _detector = GetComponent<CharacterDetector>();
        _owner = GetComponent<Creature>();
    }

    private void OnEnable() =>
        _detector.Detected += OnTargetDetected;

    private void OnDisable() =>
        _detector.Detected -= OnTargetDetected;

    private void OnTargetDetected(Character character)
    {
        var direction = character.transform.position - transform.position;
        _owner.SetDirection(direction.normalized);
    }
}
