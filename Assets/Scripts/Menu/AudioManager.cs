using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioSource> bgmSources = new List<AudioSource>();
    public List<AudioSource> sfxSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Configurar el volumen global al iniciar el juego
            AudioListener.volume = PlayerPrefs.GetFloat("volume", 100) / 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Reproduce la m�sica adecuada dependiendo de la escena activa
        PlaySceneBGM();

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reproduce la m�sica adecuada cuando una nueva escena se carga
        PlaySceneBGM();
    }

    private void PlaySceneBGM()
    {
        // Detener cualquier m�sica que est� sonando
        StopBGM();

        // Determinar qu� m�sica reproducir en funci�n de la escena actual
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Menu")
        {
            PlayBGM(bgmSources[1].clip, 1); // Reproducir la m�sica del men�
        }
        else if (sceneName == "Game")
        {
            PlayBGM(bgmSources[0].clip, 0); // Reproducir la m�sica del juego
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource sfxSource = GetAvailableSFXSource();
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(AudioClip clip, int sourceIndex)
    {
        StopBGM(); // Detiene toda la m�sica de fondo

        if (sourceIndex < 0 || sourceIndex >= bgmSources.Count)
        {
            Debug.LogWarning("�ndice de fuente BGM inv�lido.");
            return;
        }

        AudioSource bgmSource = bgmSources[sourceIndex];
        if (bgmSource != null)
        {
            if (bgmSource.clip != clip)
            {
                bgmSource.clip = clip;
            }
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        foreach (var bgmSource in bgmSources)
        {
            bgmSource.Stop();
        }
    }

    public void StopSFX()
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.Stop();
        }
    }

    private AudioSource GetAvailableBGMSource()
    {
        foreach (var source in bgmSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume / 100;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }
}
