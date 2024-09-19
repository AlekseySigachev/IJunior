using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private int _waypointIndex;

    private void Update()
    {
        Move();

        if (IsWaypointReached())
            UpdateWaypointIndex();
    }

    private void Move() => transform.position = Vector3.MoveTowards(transform.position, _waypoints[_waypointIndex].position, _speed * Time.deltaTime);

    private bool IsWaypointReached() => transform.position == _waypoints[_waypointIndex].position;

    private void UpdateWaypointIndex() => _waypointIndex = ++_waypointIndex % _waypoints.Length;
}