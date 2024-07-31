using System.Collections;
using UnityEngine;

public class PlayerVisualDamage : MonoBehaviour
{
    public float flickDuration = 0.1f; // Duración del parpadeo
    public int flickCount = 5; // Cantidad de parpadeos
    private SpriteRenderer spriteRenderer;
    private PlayerHealth playerHealth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TriggerDamageFlick()
    {
        StartCoroutine(DamageFlicker());
    }

    private IEnumerator DamageFlicker()
    {
        for (int i = 0; i < flickCount; i++)
        {
            spriteRenderer.enabled = false; // Apagar el sprite
            yield return new WaitForSeconds(flickDuration);
            spriteRenderer.enabled = true; // Encender el sprite
            yield return new WaitForSeconds(flickDuration);
        }
    }
}
