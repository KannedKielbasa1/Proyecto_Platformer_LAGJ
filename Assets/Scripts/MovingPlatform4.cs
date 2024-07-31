using UnityEngine;

public class MovingPlatform4 : MonoBehaviour
{
    public float fallSpeed = 2f; // VELOCIDAD A LA QUE CAE LA PLATAFORMA
    public float vanishDelay = 2f; // TIEMPO ANTES DE QUE DESAPAREZCA
    public float vanishDuration = 1f; // TIEMPO QUE LE TOMARÁ PARA DESAPARECER COMPLETAMENTE
    public float respawnDelay = 1f; // TIEMPO DE RESPAWN DESPUÉS DE DESAPARECER
    public float damageAmount = 25f; // CANTIDAD DE VIDA QUE SE RESTARÁ AL JUGADOR

    private float vanishTimer = 0f;
    private bool isFalling = true;

    private Renderer platformRenderer;
    private Collider2D platformCollider;
    private Color originalColor;
    private Vector3 originalPosition;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider2D>(); // Obtener el Collider2D de la plataforma
        originalColor = platformRenderer.material.color;
        originalPosition = transform.position;

        // COMENZAR EL EFECTO DE CAIDA DE FORMA AUTOMÁTICA
        StartFalling();
    }

    void Update()
    {
        if (isFalling)
        {
            // HACER QUE LA PLATAFORMA CAIGA
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            // COMENZAR EL EFECTO DE VANISH DESPUÉS DEL DELAY
            vanishTimer += Time.deltaTime;
            if (vanishTimer >= vanishDelay)
            {
                float alpha = Mathf.Lerp(1f, 0f, (vanishTimer - vanishDelay) / vanishDuration);
                Color newColor = originalColor;
                newColor.a = alpha;
                platformRenderer.material.color = newColor;

                // DESACTIVAR EL COLLIDER2D
                platformCollider.enabled = alpha > 0f;

                // REINICIAR LA PLATAFORMA DESPUÉS DE QUE DESAPAREZCA
                if (alpha <= 0f)
                {
                    isFalling = false;
                    Invoke("Respawn", respawnDelay);
                }
            }
        }
    }

    private void StartFalling()
    {
        isFalling = true;
        vanishTimer = 0f;
        platformRenderer.material.color = originalColor;
        platformCollider.enabled = true; // Asegurarse de que el collider esté activado al comenzar
    }

    private void Respawn()
    {
        transform.position = originalPosition;
        StartFalling();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto con el que colisiona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el script PlayerHealth del jugador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Reducir la vida del jugador en un 25%
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
