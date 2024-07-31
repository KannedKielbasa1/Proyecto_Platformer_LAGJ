using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Variable p�blica para la vida m�xima del jugador
    public float maxHealth = 100f;
    // Variable privada para la vida actual del jugador
    private float currentHealth;

    void Start()
    {
        // Inicializar la vida actual al valor m�ximo al comenzar el juego
        currentHealth = maxHealth;
    }

    // M�todo para reducir la vida del jugador
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // Asegurarse de que la vida no caiga por debajo de cero
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Verificar si el jugador ha muerto
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // M�todo para incrementar la vida del jugador (curaci�n)
    public void Heal(float amount)
    {
        currentHealth += amount;
        // Asegurarse de que la vida no exceda la vida m�xima
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    // M�todo para manejar la muerte del jugador
    private void Die()
    {
        // Aqu� puedes manejar lo que sucede cuando el jugador muere
        Debug.Log("Player has died.");
        // Por ejemplo, puedes recargar la escena, mostrar una pantalla de game over, etc.
    }

    // M�todo para obtener el valor actual de la vida del jugador
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
