using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private float _distanceToTarget = 0.1f;
    private int _currentWaypointIndex;

    private void Start()
    {
        _currentWaypointIndex = 0;
    }

    private void Update()
    {
        if (IsCurrentWayPointReached())
            UpdateCurrentWaypointIndex();
        else
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
    }

    private bool IsCurrentWayPointReached() => transform.position.IsEnoughClose(_waypoints[_currentWaypointIndex].position, _distanceToTarget);

    private void UpdateCurrentWaypointIndex() => _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
}
