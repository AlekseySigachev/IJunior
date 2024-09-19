using UnityEngine;

public class Enemy : MoveableObject
{
    [SerializeField] protected float Speed = 2.5f;

    private Transform _target;

    private void Update()
    {
        Move(transform, _target, Speed);
    }

    public void SetTarget(Transform target) => _target = target;
}
