using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public SpriteRenderer _sprite;

    private CapsuleCollider2D capsuleCollider;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isGrounded;
    private int maxJumps = 1;
    private int jumpsMade = 0;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Jump.performed += ctx => TryJump();
        controls.Player.Jump.canceled += ctx => EndJump();
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
        // Movimiento horizontal
        Vector2 horizontalMove = new Vector2(moveInput.x * speed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalMove.x, rb.velocity.y);

        // Actualizar la animación
        animator.SetBool("IsMoving", moveInput.x != 0);
        if (moveInput.x > 0)
        {
            if (_sprite != null)
            {
                _sprite.flipX =true;
            }
        }
        else if (moveInput.x < 0)
        {
            if (_sprite != null)
            {
                _sprite.flipX = false;
            }
        }

        // Verificar si está en el suelo
        isGrounded = CheckGrounded();

        // Resetear el contador de saltos si está en el suelo
        if (isGrounded)
        {
            jumpsMade = 0;
        }
    }

    private void TryJump()
    {
        if (isGrounded && jumpsMade < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsMade++; // Incrementar el contador de saltos
            isGrounded = false; // Marcar que ya no está en el suelo
        }
    }

    private void EndJump()
    {
        // Este método se puede utilizar para manejar la liberación del botón de salto si es necesario
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
}