using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;

    private float screenHalfWidthInWorldUnits;
    private float screenHalfHeightInWorldUnits;

    private PlayerControls controls;
    private Vector2 moveInput;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        // Calcular la mitad de la pantalla en unidades del mundo
        Camera cam = Camera.main;
        Vector3 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        screenHalfWidthInWorldUnits = screenBounds.x;
        screenHalfHeightInWorldUnits = screenBounds.y;
    }

    void Update()
    {
        // Movimiento horizontal
        Vector3 horizontal = new Vector3(moveInput.x * speed * Time.deltaTime, 0.0f, 0.0f);
        transform.position += horizontal;

        // Loop para pasar al personaje al otro lado de la pantalla
        Vector3 newPosition = transform.position;

        if (newPosition.x < -screenHalfWidthInWorldUnits)
        {
            newPosition.x = screenHalfWidthInWorldUnits;
        }
        else if (newPosition.x > screenHalfWidthInWorldUnits)
        {
            newPosition.x = -screenHalfWidthInWorldUnits;
        }

        if (newPosition.y < -screenHalfHeightInWorldUnits)
        {
            newPosition.y = screenHalfHeightInWorldUnits;
        }
        else if (newPosition.y > screenHalfHeightInWorldUnits)
        {
            newPosition.y = -screenHalfHeightInWorldUnits;
        }

        transform.position = newPosition;

        // Actualizar la animación
        animator.SetFloat("Horizontal", moveInput.x);
    }
}
