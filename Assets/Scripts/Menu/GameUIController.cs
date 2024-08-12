using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public CoinManager coinManager; // Referencia al CoinManager
    public PlayerHealth playerHealth; // Referencia al PlayerHealth
    public CountdownTimer timer; // Referencia al CountdownTimer

    private void Start()
    {
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
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
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Congela la escena
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Restablece el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // Asegura que el tiempo se reanuda al salir
        SceneManager.LoadScene("Menu");
    }
}
