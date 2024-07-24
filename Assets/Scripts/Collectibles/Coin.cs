using UnityEngine;

// Clase derivada para las monedas
public class Coin : Collectible
{
    public override void Collect()
    {
        base.Collect(); // Llama al m�todo de la clase base
        CoinManager.Instance.AddCoin(); // Agrega una moneda al contador
        Destroy(gameObject); // Destruye la moneda despu�s de ser recogida
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
