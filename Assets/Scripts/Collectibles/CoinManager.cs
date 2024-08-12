using UnityEngine;
using UnityEngine.UI;

// Clase para manejar el conteo de monedas
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int coinCount = 0; // Contador de monedas
    public Text coinText; // Referencia al componente de texto UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinText(); // Asegura que el texto esté actualizado al iniciar
    }

    // Método para agregar una moneda al contador
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText(); // Actualiza el texto cada vez que se recolecta una moneda
    }

    // Método para actualizar el texto del contador de monedas
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.LogWarning("Coin text UI component is not assigned!");
        }
    }

    // Nueva propiedad para acceder al contador de monedas
    public int CoinsCollected
    {
        get { return coinCount; }
    }
}
