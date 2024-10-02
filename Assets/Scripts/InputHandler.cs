using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Character _character;

    private UserInput _userInput;

    private void Awake()
    {
        _userInput = new UserInput();

        _userInput.Character.Movement.performed += OnMovement;
        _userInput.Character.Movement.canceled += OnMovement;

        _userInput.Character.Attack.started += OnAttack;
    }

    private void OnEnable() =>
        _userInput.Enable();

    private void OnMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        _character.SetDirection(direction);
    }

    private void OnAttack(InputAction.CallbackContext context) 
    {
        _character.StartAttackAnimation();
    }
}
