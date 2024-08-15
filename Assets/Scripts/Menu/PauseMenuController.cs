using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public BasicMovement basicMovement; // Referencia al script de movimiento
    public PlayerAttack playerAttack; // Referencia al script de ataque

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        DisablePlayerInputs(); // Desactiva los inputs del jugador
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        EnablePlayerInputs(); // Vuelve a habilitar los inputs del jugador
        isPaused = false;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
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

    public bool IsPaused()
    {
        return isPaused;
    }

}
