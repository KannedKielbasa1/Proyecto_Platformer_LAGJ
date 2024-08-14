using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject soundOptionsPanel;
    public Slider volumeSlider;
    public Text volumeText;
    public GameObject menuPanel;

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
        menuPanel.SetActive(false);
    }

    public void SaveSoundOptions()
    {
        // Guardar el valor de volumen
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();

        soundOptionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void CancelSoundOptions()
    {
        // Revertir a la configuración guardada y cerrar la ventana
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 100);
        soundOptionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnVolumeSliderChanged()
    {
        UpdateVolumeText(volumeSlider.value);
        AudioManager.Instance.UpdateVolume(volumeSlider.value); // Actualiza el volumen en tiempo real
    }

    private void UpdateVolumeText(float volume)
    {
        volumeText.text = volume.ToString("0") + "%";
    }
}
