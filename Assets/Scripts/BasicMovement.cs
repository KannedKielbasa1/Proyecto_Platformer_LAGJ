using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isGrounded;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Jump.performed += ctx => Jump();
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
    }

    void Update()
    {
        // Movimiento horizontal
        Vector2 horizontalMove = new Vector2(moveInput.x * speed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalMove.x, rb.velocity.y);

        // Actualizar la animación
        animator.SetFloat("Horizontal", moveInput.x);

        // Verificar si está en el suelo
        isGrounded = CheckGrounded();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool CheckGrounded()
    {
        // Usar el colisionador para detectar si está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + 0.1f);
        return hit.collider != null;
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
