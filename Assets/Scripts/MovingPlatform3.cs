using UnityEngine;
using System.Collections;

public class MovingPlatform3 : MonoBehaviour
{
    public float fadeDuration = 2f; // DURACION DEL FADE
    private SpriteRenderer spriteRenderer;
    private bool isFadingIn;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        while (true)
        {
            yield return Fade(0f, 1f); // FADEIN
            yield return new WaitForSeconds(1f); // ESPERAR POR UN SEGUNDO PARA QUE SEA VISIBLE
            yield return Fade(1f, 0f); // FADEOUT
            yield return new WaitForSeconds(1f); // LO MISMO DE ARRIBA
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }

        // ACUERDATE QUE EL ALPHA ESTE BIEN PUESTO AHHHHHHHHHHHHHHHHH
        Color finalColor = spriteRenderer.color;
        finalColor.a = endAlpha;
        spriteRenderer.color = finalColor;
    }
}
