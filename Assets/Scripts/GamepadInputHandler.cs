using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInputHandler : MonoBehaviour
{
    private PlayerControls controls;
    public BasicMovement basicMovement;
    public PlayerAttack playerAttack;
    public PauseMenuController pauseMenuController;

    void Awake()
    {
        controls = new PlayerControls();

        // Moverse izquierda/derecha
        controls.Player.Move.performed += ctx => basicMovement.SetMoveInput(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => basicMovement.SetMoveInput(Vector2.zero);

        // Saltar
        controls.Player.Jump.performed += ctx => basicMovement.TryJump();
        controls.Player.Jump.canceled += ctx => basicMovement.EndJump();

        // Doble Salto
        controls.Player.Jump.performed += ctx => basicMovement.HandleDoubleJump();

        // Sprint
        controls.Player.Sprint.performed += ctx => basicMovement.StartSprint();
        controls.Player.Sprint.canceled += ctx => basicMovement.StopSprint();

        // Dash
        controls.Player.Dash.performed += ctx => basicMovement.PerformDash();

        // Ataque
        controls.Player.Attack.performed += ctx => playerAttack.Attack();

        // Pausar
        controls.Player.Pause.performed += ctx =>
        {
            if (pauseMenuController.IsPaused())
            {
                pauseMenuController.ResumeGame();
            }
            else
            {
                pauseMenuController.PauseGame();
            }
        };
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
