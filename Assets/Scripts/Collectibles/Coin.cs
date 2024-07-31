using UnityEngine;

public class Coin : Collectible
{
    public AudioClip coinSound; // Referencia al clip de sonido de la moneda

    public override void Collect()
    {
        base.Collect(); // Llama al método de la clase base
        CoinManager.Instance.AddCoin(); // Agrega una moneda al contador
        PlaySound(); // Reproduce el sonido
        Destroy(gameObject); // Destruye la moneda después de ser recogida
    }

    // Reproduce el efecto de sonido usando AudioManager
    private void PlaySound()
    {
        if (coinSound != null)
        {
            AudioManager.Instance.PlaySFX(coinSound); // Usa el AudioManager para reproducir el sonido
            Debug.Log("Playing coin collection sound.");
        }
        else
        {
            Debug.LogWarning("Coin sound is not assigned in the Coin script.");
        }
    }

    // Detecta la colisión con el jugador
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(); // Recoge la moneda
        }
    }
}
