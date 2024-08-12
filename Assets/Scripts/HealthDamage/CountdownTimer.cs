using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 20f; // Tiempo inicial del cronómetro
    private float currentTime; // Tiempo actual del cronómetro
    private bool timerRunning = false; // Estado del cronómetro

    public Text timerText; // Referencia al componente de texto para mostrar el tiempo

    private PlayerHealth playerHealth; // Referencia al script PlayerHealth

    void Start()
    {
        currentTime = startTime; // Inicializar el tiempo actual con el tiempo inicial
        playerHealth = FindObjectOfType<PlayerHealth>(); // Obtener referencia al script PlayerHealth
        timerRunning = true; // Iniciar el cronómetro
    }

    void Update()
    {
        if (timerRunning)
        {
            // Reducir el tiempo actual
            currentTime -= Time.deltaTime;
            // Actualizar el texto del cronómetro
            UpdateTimerText(currentTime);

            // Verificar si el tiempo ha llegado a cero
            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                if (playerHealth != null)
                {
                    playerHealth.Die(); // Matar al jugador
                }
            }
        }
    }

    void UpdateTimerText(float time)
    {
        // Convertir el tiempo a minutos y segundos y actualizar el texto
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Nueva propiedad para acceder al tiempo actual
    public float CurrentTime
    {
        get { return currentTime; }
    }
}
