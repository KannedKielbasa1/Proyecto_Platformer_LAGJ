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
        UpdateAllAudioSourcesVolume(savedVolume / 100f);
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
        float savedVolume = PlayerPrefs.GetFloat("volume", 100);
        volumeSlider.value = savedVolume;
        UpdateVolumeText(savedVolume);
        UpdateAllAudioSourcesVolume(savedVolume / 100f);

        soundOptionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnVolumeSliderChanged()
    {
        UpdateVolumeText(volumeSlider.value);
        UpdateAllAudioSourcesVolume(volumeSlider.value / 100f);
    }

    private void UpdateVolumeText(float volume)
    {
        volumeText.text = volume.ToString("0") + "%";
    }

    private void UpdateAllAudioSourcesVolume(float volume)
    {
        // Actualiza el volumen de todos los AudioSources en la escena
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
}
