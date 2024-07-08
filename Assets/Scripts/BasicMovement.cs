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
    private int maxJumps = 2; // PERMITIR DOS SALTOS
    private int jumpsMade = 0;
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
        // DETERMINAR VELOCIDAD
        float currentSpeed = (isSprinting && (moveInput.x != 0)) ? sprintValue : speed;

        // MOVIMIENTO HORIZONTAL
        Vector2 horizontalMove = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalMove.x, rb.velocity.y);

        // ACTUALIZAR LA ANIMACIÓN
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

        // VERIFICAR QUE ESTÉ EN EL SUELO
        isGrounded = CheckGrounded();

        // RESTEAR EL CONTADOR DE SALTOS UNA VEZ EN EL SUELO
        if (isGrounded)
        {
            jumpsMade = 0;
        }

        // ACTUALIZAR ESTADO DEL DASH
        if (isDashing)
        {
            ContinueDash();
        }
    }

    private void TryJump()
    {
        if (jumpsMade < maxJumps)
        {
            float currentJumpForce = jumpsMade == 0 ? jumpForce : doubleJumpForce;
            rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
            jumpsMade++; // INCREMENTAR EL CONTADOR DE SALTOS
            isGrounded = false; // MARCAR QUE YA NO ESTÁ EN EL SUELO

            // ACTIVAR ANIMACION DE SALTO
            animator.SetTrigger("JumpTrigger");
        }
    }

    private void EndJump()
    {
        // METODO PARA MANEJAR LA LIBERACIÓN DE SALTO SI ES NECESARIO
    }

    private bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
        animator.SetTrigger("Dash"); // ACTIVAR LA ANIMACION DE DASH
        Debug.Log("Dash Triggered"); // AGREGAR LOG PARA VERIFICAR QUE FUNCIONE EL DASH
    }

    void ContinueDash()
    {
        if (Time.time >= dashTime)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
            Debug.Log("Dash Ended"); // LOG PARA VERIFICAR QUE DASH TERMINO
        }
        else
        {
            rb.velocity = dashDirection * dashSpeed;
            Debug.Log("Dashing"); // LOG PARA VERIFICAR QUE ESTE HACIENDO DASH
        }
    }
}
