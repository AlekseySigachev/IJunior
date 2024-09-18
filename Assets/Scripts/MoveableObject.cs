using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    protected virtual void Move(Transform current, Transform target, float speed) 
    {
        current.position = Vector3.MoveTowards(current.position, target.position, speed * Time.deltaTime);
    }
}
