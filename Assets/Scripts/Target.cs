using UnityEngine;

public class Target : MoveableObject
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

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

    private bool IsCurrentWayPointReached() => Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position) <= 0.01f;

    private void UpdateCurrentWaypointIndex() => _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
}
