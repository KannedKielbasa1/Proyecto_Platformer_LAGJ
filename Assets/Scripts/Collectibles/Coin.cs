using UnityEngine;

// Clase derivada para las monedas
public class Coin : Collectible
{
    private AudioSource audioSource; // Referencia al componente de AudioSource

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the Coin prefab!");
        }
    }

    public override void Collect()
    {
        base.Collect(); // Llama al m�todo de la clase base
        CoinManager.Instance.AddCoin(); // Agrega una moneda al contador
        PlaySound(); // Reproduce el sonido
        Destroy(gameObject); // Destruye la moneda despu�s de ser recogida
    }

    // Reproduce el efecto de sonido
    private void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip); // Reproduce el sonido del clip asignado
            Debug.Log("Playing coin collection sound.");
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned or found on the Coin.");
        }
    }

    // Detecta la colisi�n con el jugador
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(); // Recoge la moneda
        }
    }
}
