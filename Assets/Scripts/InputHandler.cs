using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Character _character;

    private void OnMovement(InputValue context)
    {
        var direction = context.Get<Vector2>();
        _character.SetDirection(direction);
    }
}
