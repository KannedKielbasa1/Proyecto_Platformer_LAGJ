using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    [SerializeField] private float sprintValue = 8.0f; // VALOR DEL SPRINT
    public float jumpForce = 9.0f; // FUERZA DE SALTO NORMAL
    public float doubleJumpForce = 6.0f; // FUERZA DEL DOBLE SALTO
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public SpriteRenderer _sprite;

    private CapsuleCollider2D capsuleCollider;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isSprinting = false;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private float dashTime;
    private float nextDashTime;
    private bool isDashing;
    private Vector2 dashDirection;

    private float lastDashPressTimeA = 0f;
    private float lastDashPressTimeD = 0f;
    private float doublePressThreshold = 0.3f; // TIEMPO PERMITIDO PARA CONSIDERAR DOBLE PULSACIÓN

    public Transform wallCheck1; // Primer punto de verificación de pared
    public Transform wallCheck2; // Segundo punto de verificación de pared
    public LayerMask wallLayer; // Capa que define las paredes

    private bool isTouchingWall = false; // Nuevo estado para verificar si se toca una pared
    private int wallJumpLimit = 1; // Limitar la cantidad de saltos desde una pared
    private int wallJumpsMade = 0; // Contador para los saltos realizados desde una pared

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Jump.performed += ctx => TryJump();
        controls.Player.Jump.canceled += ctx => EndJump();
        controls.Player.Sprint.performed += ctx => StartSprint();
        controls.Player.Sprint.canceled += ctx => StopSprint();
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
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (_sprite == null)
        {
            _sprite = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Verificar si está tocando una pared
        isTouchingWall = CheckWallTouch();

        // Determinar velocidad
        float currentSpeed = (isSprinting && (moveInput.x != 0)) ? sprintValue : speed;

        // Movimiento horizontal
        Vector2 horizontalMove = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalMove.x, rb.velocity.y);

        // Actualizar la animación
        animator.SetBool("IsMoving", moveInput.x != 0);
        if (moveInput.x > 0)
        {
            if (_sprite != null)
            {
                _sprite.flipX = true;
            }
            CheckForDash(KeyCode.D, ref lastDashPressTimeD);
        }
        else if (moveInput.x < 0)
        {
            if (_sprite != null)
            {
                _sprite.flipX = false;
            }
            CheckForDash(KeyCode.A, ref lastDashPressTimeA);
        }

        // Verificar que esté en el suelo
        isGrounded = CheckGrounded();

        // Restablecer el contador de saltos si está en el suelo
        if (isGrounded)
        {
            canDoubleJump = true;
            wallJumpsMade = 0; // Restablecer el contador de saltos de pared
        }

        // Actualizar estado isGrounded en el animator
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("Jumping", !isGrounded);

        // Actualizar estado del dash
        if (isDashing)
        {
            ContinueDash();
        }
    }

    private void TryJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
        else if (isTouchingWall && wallJumpsMade < wallJumpLimit)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            wallJumpsMade++;
        }

        // Activar animación de salto si es necesario
    }

    private void EndJump()
    {
        // Método para manejar la liberación de salto si es necesario
    }

    private bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool CheckWallTouch()
    {
        // Verificar si alguno de los wallChecks está tocando una pared
        bool wallCheck1Touch = Physics2D.OverlapCircle(wallCheck1.position, 0.2f, wallLayer);
        bool wallCheck2Touch = Physics2D.OverlapCircle(wallCheck2.position, 0.2f, wallLayer);
        return wallCheck1Touch || wallCheck2Touch;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }

    private void StartSprint()
    {
        isSprinting = true;
    }

    private void StopSprint()
    {
        isSprinting = false;
    }

    private void CheckForDash(KeyCode key, ref float lastPressTime)
    {
        if (Input.GetKeyDown(key))
        {
            if (Time.time - lastPressTime < doublePressThreshold && Time.time >= nextDashTime)
            {
                StartDash(new Vector2(key == KeyCode.D ? 1 : -1, 0));
            }
            lastPressTime = Time.time;
        }
    }

    void StartDash(Vector2 direction)
    {
        isDashing = true;
        dashTime = Time.time + dashDuration;
        nextDashTime = Time.time + dashCooldown;
        dashDirection = direction;
        animator.SetTrigger("Dash"); // Activar la animación de dash
        Debug.Log("Dash Triggered"); // Agregar log para verificar que funcione el dash
    }

    void ContinueDash()
    {
        if (Time.time >= dashTime)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
            Debug.Log("Dash Ended"); // Log para verificar que dash terminó
        }
        else
        {
            rb.velocity = dashDirection * dashSpeed;
            Debug.Log("Dashing"); // Log para verificar que esté haciendo dash
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.2f);
        Gizmos.DrawSphere(wallCheck1.position, 0.2f);
        Gizmos.DrawSphere(wallCheck2.position, 0.2f);
    }
}
