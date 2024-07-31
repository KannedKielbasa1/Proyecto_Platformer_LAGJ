using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    // Referencia al texto de UI que muestra la vida
    public Text healthText;
    // Referencia al script de salud del jugador
    public PlayerHealth playerHealth;

    void Update()
    {
        // Obtener la vida actual del jugador como porcentaje
        float currentHealthPercentage = (playerHealth.GetCurrentHealth() / playerHealth.maxHealth) * 100;
        // Actualizar el texto de UI con la vida actual
        healthText.text = "Health: " + currentHealthPercentage.ToString("F0") + "%";
    }
}
