using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform attackPoint; // Punto desde donde se lanzar� el proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public AudioClip[] attackSounds; // Arreglo de sonidos de ataque
    private AudioSource audioSource; // Componente AudioSource para reproducir sonidos
    private Animator animator; // Referencia al Animator
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        // Asignar el evento de ataque al bot�n izquierdo del rat�n
        controls.Player.Attack.performed += ctx => Attack();

        // Obtener el Animator y SpriteRenderer desde el GameObject
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource del objeto
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    public void Attack()
    {
        // Activar la animaci�n de ataque
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Instanciar el proyectil en el punto de ataque
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
        var projec = projectile.GetComponent<Projectile>();
        projec.lifetime = 5;
        projec.Init();

        // Obtener la direcci�n del proyectil basado en la orientaci�n del jugador
        Vector2 direction = spriteRenderer.flipX ? Vector2.right : Vector2.left;

        // Asignar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // Reproducir un sonido de ataque aleatorio
        if (attackSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, attackSounds.Length);
            audioSource.PlayOneShot(attackSounds[randomIndex]);
        }
    }
}
