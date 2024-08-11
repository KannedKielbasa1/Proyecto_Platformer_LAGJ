using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Detener el modo Play en el editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Cerrar el juego si está compilado
        Application.Quit();
#endif
    }
}
