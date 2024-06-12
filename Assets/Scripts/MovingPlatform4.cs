using UnityEngine;

public class MovingPlatform4 : MonoBehaviour
{
    public float fallSpeed = 2f; // VELOCIDAD A LA QUE CAE LA PLATAFORMA
    public float vanishDelay = 2f; // TIEMPO ANTES DE QUE DESAPAREZCA
    public float vanishDuration = 1f; // TIEMPO QUE LE TOMA PARA DESAPARECER COMPLETAMENTE
    public float respawnDelay = 1f; // TIEMPO DE RESPAWN DESPUES DE DESAPARECER
    private float vanishTimer = 0f;
    private bool isFalling = true;

    private Renderer platformRenderer;
    private Color originalColor;
    private Vector3 originalPosition;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        originalColor = platformRenderer.material.color;
        originalPosition = transform.position;

        // COMENZAR EL EFECTO DE CAIDA DE FORMA AUTOMATICAAAAAAA
        StartFalling();
    }

    void Update()
    {
        if (isFalling)
        {
            // HACER QUE LA PLATAFORMA CAIGA
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            // COMENZAR EL EFECTO DE VANISH DESPUES DEL DELAY
            vanishTimer += Time.deltaTime;
            if (vanishTimer >= vanishDelay)
            {
                float alpha = Mathf.Lerp(1f, 0f, (vanishTimer - vanishDelay) / vanishDuration);
                Color newColor = originalColor;
                newColor.a = alpha;
                platformRenderer.material.color = newColor;

                // REINICIAR LA PLATAFORMA DESPUES DE QUE DESAPAREZCA
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
    }

    private void Respawn()
    {
        transform.position = originalPosition;
        StartFalling();
    }
}
