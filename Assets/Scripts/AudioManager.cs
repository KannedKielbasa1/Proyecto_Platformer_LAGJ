using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // PROPIEDADES
    public float volume { get; private set; }
    public bool isMuted { get; private set; }
    public string currentTrack { get; private set; }
    public float soundEffectsVolume { get; private set; }
    public float musicVolume { get; private set; }

    // REFERENCIAS A LOS AUDIOS
    private AudioSource musicSource;
    private AudioSource soundEffectsSource;

    void Awake()
    {
        // CONFIGURA AUDIOMANAGER COMO SINGLETON
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        // INICIA LOS AUDIOSOURCES
        musicSource = gameObject.AddComponent<AudioSource>();
        soundEffectsSource = gameObject.AddComponent<AudioSource>();

        // VALORES INICIALES
        volume = 1.0f;
        soundEffectsVolume = 1.0f;
        musicVolume = 1.0f;
        isMuted = false;
    }

    // METODOS
    public void PlaySound(string name)
    {
        // CARGAR Y REPRODUCIR SONIDO
        AudioClip clip = Resources.Load<AudioClip>(name);
        if (clip != null)
        {
            soundEffectsSource.PlayOneShot(clip, soundEffectsVolume);
        }
        else
        {
            Debug.LogWarning("Sonido no encontrado: " + name);
        }
    }

    public void StopSound(string name)
    {
        // 
        soundEffectsSource.Stop();
    }

    public void SetVolume(float volume)
    {
        this.volume = Mathf.Clamp(volume, 0f, 1f);
        AudioListener.volume = this.volume;
    }

    public void Mute()
    {
        isMuted = true;
        AudioListener.volume = 0f;
    }

    public void Unmute()
    {
        isMuted = false;
        AudioListener.volume = volume;
    }

    public void PlayMusic(string track)
    {
        // CARGAR Y REPRODUCIR LA MUSICA
        AudioClip clip = Resources.Load<AudioClip>(track);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = musicVolume;
            musicSource.loop = true;
            musicSource.Play();
            currentTrack = track;
        }
        else
        {
            Debug.LogWarning("Musica no encontrada: " + track);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        currentTrack = null;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsVolume = Mathf.Clamp(volume, 0f, 1f);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp(volume, 0f, 1f);
        if (musicSource.isPlaying)
        {
            musicSource.volume = musicVolume;
        }
    }
}
