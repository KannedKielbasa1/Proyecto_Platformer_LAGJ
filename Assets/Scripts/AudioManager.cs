using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioSource> bgmSources = new List<AudioSource>(); // Lista de fuentes BGM
    public List<AudioSource> sfxSources = new List<AudioSource>(); // Lista de fuentes SFX

    private void Awake()
    {
        // Patron singleton para asegurar que el manager exista
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reproducir un efecto de sonido en la primera fuente de SFX disponible
    public void PlaySFX(AudioClip clip)
    {
        AudioSource sfxSource = GetAvailableSFXSource();
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Reproducir musica de fondo en la primera fuente de BGM disponible
    public void PlayBGM(AudioClip clip)
    {
        AudioSource bgmSource = GetAvailableBGMSource();
        if (bgmSource != null)
        {
            if (bgmSource.clip == clip) return;

            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    // Detener toda la musica de fondo
    public void StopBGM()
    {
        foreach (var bgmSource in bgmSources)
        {
            bgmSource.Stop();
        }
    }

    // Detener todos los efectos de sonido
    public void StopSFX()
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.Stop();
        }
    }

    // Obtener la primera fuente de BGM disponible
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

    // Obtener la primera fuente de SFX disponible
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
}
