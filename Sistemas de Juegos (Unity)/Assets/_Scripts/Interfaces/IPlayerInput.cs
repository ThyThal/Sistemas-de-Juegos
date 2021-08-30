using UnityEngine.InputSystem;

public interface IPlayerInput
{
    void OnMovement(InputAction.CallbackContext value);
    void OnAttack(InputAction.CallbackContext value);
}
