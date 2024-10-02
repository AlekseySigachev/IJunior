using System.Collections;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _treshhold;

    private Creature _creature;
    private int _destinationPointIndex;
    private Coroutine _coroutine;


    private void Awake()
    {
        _creature = GetComponent<Creature>();
        _coroutine = StartCoroutine(DoPatrol());
    }

    public void StartDoPatrol() =>
        _coroutine = StartCoroutine(DoPatrol());

    public void StopDoPatrol()
    {
        StopCoroutine(_coroutine);
        _creature.SetDirection(Vector2.zero);
    }

    private IEnumerator DoPatrol()
    {
        while (enabled)
        {
            if (IsOnPoint())
                _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);

            var direction = _points[_destinationPointIndex].position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);
            yield return null;
        }
    }

    private bool IsOnPoint() =>
        (_points[_destinationPointIndex].position - transform.position).magnitude < _treshhold;
}
