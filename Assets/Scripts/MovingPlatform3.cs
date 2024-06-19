using UnityEngine;
using System.Collections;

public class MovingPlatform3 : MonoBehaviour
{
    public float fadeDuration = 2f; // Duración del fade
    private SpriteRenderer spriteRenderer;
    private EdgeCollider2D edgeCollider;
    private bool isFadingIn;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        while (true)
        {
            yield return Fade(0f, 1f); // Fade in
            yield return new WaitForSeconds(1f); // Esperar por un segundo para que sea visible
            yield return Fade(1f, 0f); // Fade out
            yield return new WaitForSeconds(1f); // Lo mismo de arriba
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Activar/desactivar colisión al inicio del fade
        edgeCollider.enabled = endAlpha > 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }

        // Asegurarse de que el alpha esté bien puesto
        Color finalColor = spriteRenderer.color;
        finalColor.a = endAlpha;
        spriteRenderer.color = finalColor;

        // Activar/desactivar colisión al final del fade
        edgeCollider.enabled = endAlpha > 0f;
    }
}
