using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject soundOptionsPanel;
    public Slider volumeSlider;
    public Text volumeText;  // Agrega un campo para el texto del volumen
    public GameObject menuPanel;  // El panel del menú principal

    private void Start()
    {
        // Cargar el valor de volumen guardado al iniciar la escena
        float savedVolume = PlayerPrefs.GetFloat("volume", 100);
        volumeSlider.value = savedVolume;
        UpdateVolumeText(savedVolume);
    }

    public void OpenSoundOptions()
    {
        soundOptionsPanel.SetActive(true);
        menuPanel.SetActive(false);  // Oculta el menú principal cuando se abren las opciones
    }

    public void SaveSoundOptions()
    {
        // Guardar el valor de volumen
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();

        soundOptionsPanel.SetActive(false);
        menuPanel.SetActive(true);  // Vuelve al menú principal
    }

    public void CancelSoundOptions()
    {
        // Revertir a la configuración guardada y cerrar la ventana
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 100);
        soundOptionsPanel.SetActive(false);
        menuPanel.SetActive(true);  // Vuelve al menú principal
    }

    public void OnVolumeSliderChanged()
    {
        UpdateVolumeText(volumeSlider.value);
    }

    private void UpdateVolumeText(float volume)
    {
        volumeText.text = volume.ToString("0") + "%";
    }
}
