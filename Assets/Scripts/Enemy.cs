using UnityEngine;

public class Enemy : MoveableObject
{
    [SerializeField] private float _speed = 2.5f;

    private Transform _target;

    private void Update()
    {
        Move(transform, _target, _speed);
    }

    public void SetTarget(Transform target) => _target = target;
}
