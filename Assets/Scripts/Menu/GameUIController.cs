using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public CoinManager coinManager; // Referencia al CoinManager
    public PlayerHealth playerHealth; // Referencia al PlayerHealth
    public CountdownTimer timer; // Referencia al CountdownTimer
    public BasicMovement basicMovement; // Referencia al script de movimiento
    public PlayerAttack playerAttack; // Referencia al script de ataque

    private void Start()
    {
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 1f; // Asegura que el tiempo se reanuda al salir
        // Registrar el método OnSceneLoaded al evento sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Desregistrar el método OnSceneLoaded al destruir el objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // Condición de victoria: el jugador recolecta 23 monedas
        if (coinManager != null && coinManager.CoinsCollected >= 23)
        {
            ShowVictoryPanel();
        }

        // Condición de derrota: el tiempo se acaba o la vida del jugador es 0
        if ((playerHealth != null && playerHealth.GetCurrentHealth() <= 0) ||
            (timer != null && timer.CurrentTime <= 0))
        {
            ShowGameOverPanel();
        }
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f; // Congela la escena
        DisablePlayerInputs(); // Desactiva los inputs del jugador
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Congela la escena
        DisablePlayerInputs(); // Desactiva los inputs del jugador
    }

    public void Retry()
    {
        // Restablece el tiempo a la normalidad antes de recargar la escena
        Time.timeScale = 1f;

        // Cargar la escena nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // Asegura que el tiempo se reanuda al salir
        SceneManager.LoadScene("Menu");
    }

    private void DisablePlayerInputs()
    {
        basicMovement.enabled = false; // Desactiva el movimiento
        playerAttack.enabled = false;  // Desactiva los ataques
    }

    private void EnablePlayerInputs()
    {
        basicMovement.enabled = true;  // Vuelve a habilitar el movimiento
        playerAttack.enabled = true;   // Vuelve a habilitar los ataques
    }

    // Método que se llama cuando se carga la escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnablePlayerInputs(); // Reactiva los inputs del jugador
    }
}
