using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;

    private Vector3 _direction;

    private void Update()
    {
        Move();
    }

    public void SetDirection(Vector3 direction) => _direction = direction;

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
