using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform attackPoint; // Punto desde donde se lanzará el proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    private Animator animator; // Referencia al Animator
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        // Asignar el evento de ataque al botón izquierdo del ratón
        controls.Player.Attack.performed += ctx => Attack();

        // Obtener el Animator y SpriteRenderer desde el GameObject
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Attack()
    {
        // Activar la animación de ataque
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Instanciar el proyectil en el punto de ataque
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
        var projec = projectile.GetComponent<Projectile>();
        projec.lifetime = 5;
        projec.Init();

        // Obtener la dirección del proyectil basado en la orientación del jugador
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // Asignar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}
