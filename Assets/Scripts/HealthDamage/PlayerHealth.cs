using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Variable pública para la vida máxima del jugador
    public float maxHealth = 100f;
    // Variable privada para la vida actual del jugador
    private float currentHealth;

    void Start()
    {
        // Inicializar la vida actual al valor máximo al comenzar el juego
        currentHealth = maxHealth;
    }

    // Método para reducir la vida del jugador
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

    // Método para incrementar la vida del jugador (curación)
    public void Heal(float amount)
    {
        currentHealth += amount;
        // Asegurarse de que la vida no exceda la vida máxima
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    // Método para manejar la muerte del jugador
    private void Die()
    {
        // Aquí puedes manejar lo que sucede cuando el jugador muere
        Debug.Log("Player has died.");
        // Por ejemplo, puedes recargar la escena, mostrar una pantalla de game over, etc.
    }

    // Método para obtener el valor actual de la vida del jugador
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
