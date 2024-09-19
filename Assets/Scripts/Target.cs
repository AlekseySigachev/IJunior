using UnityEngine;

public class Target : MoveableObject
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private float _treshhold = 0.01f;
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
            Move(transform, _waypoints[_currentWaypointIndex], _speed);
    }

    private bool IsCurrentWayPointReached() => Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position) <= _treshhold;

    private void UpdateCurrentWaypointIndex() => _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
}
