using UnityEngine;

// Clase derivada para las monedas
public class Coin : Collectible
{
    public override void Collect()
    {
        base.Collect(); // Llama al método de la clase base
        CoinManager.Instance.AddCoin(); // Agrega una moneda al contador
        Destroy(gameObject); // Destruye la moneda después de ser recogida
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
