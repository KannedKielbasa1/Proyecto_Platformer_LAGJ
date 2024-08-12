using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    public void OnPause()
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

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Congela la escena
        isPaused = true;
    }

    private void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda la escena
        isPaused = false;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // Asegura que el tiempo se reanuda al salir
        SceneManager.LoadScene("Menu");
    }
}
