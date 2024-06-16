using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;

    private float screenHalfWidthInWorldUnits;
    private float screenHalfHeightInWorldUnits;

    private PlayerControls controls;
    private Vector2 moveInput;
    private bool jumpInput;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => jumpInput = true;
        controls.Player.Jump.canceled += ctx => jumpInput = false;
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
        // CALCULAR MITAD DE LA PANTALLA EN UNIDADES DEL MUNDO
        Camera cam = Camera.main;
        Vector3 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        screenHalfWidthInWorldUnits = screenBounds.x;
        screenHalfHeightInWorldUnits = screenBounds.y;
    }

    void Update()
    {
        // MOVIMIENTO HORIZONTAL
        Vector3 horizontal = new Vector3(moveInput.x * speed * Time.deltaTime, 0.0f, 0.0f);
        transform.position += horizontal;

        // MOVIMIENTO VERTICAL (SALTO)
        if (jumpInput)
        {
            transform.position += new Vector3(0.0f, jumpForce * Time.deltaTime, 0.0f);
            jumpInput = false; // SALTA UNA VEZ POR APACHURRAR
        }

        // LOOP PARA PASAR AL PERSONAJE AL OTRO LADO DE LA PANTALLA
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
    }
}
