using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

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

    // Reproducir un efecto de sonido
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Reproducir musica de fondo
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // Detener musica de fondo
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
