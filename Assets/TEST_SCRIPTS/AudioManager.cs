using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Propiedades
    private float volume;
    private bool isMuted;
    private string currentTrack;
    private float soundEffectsVolume;
    private float musicVolume;

    // Referencias a componentes de audio
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    // Métodos
    public void PlaySound(string name)
    {
        AudioClip clip = GetAudioClipByName(name);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, soundEffectsVolume);
        }
        else
        {
            Debug.LogWarning("Clip not found: " + name);
        }
    }

    public void StopSound(string name)
    {
        // Unity no proporciona una manera directa de detener un sonido específico, 
        // pero podemos detener todos los sonidos.
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        AudioListener.volume = volume;
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
        AudioClip clip = GetAudioClipByName(track);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.volume = musicVolume;
            audioSource.loop = true;
            audioSource.Play();
            currentTrack = track;
        }
        else
        {
            Debug.LogWarning("Track not found: " + track);
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
        currentTrack = null;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (audioSource.isPlaying && audioSource.clip.name == currentTrack)
        {
            audioSource.volume = musicVolume;
        }
    }

    // Método auxiliar para encontrar un AudioClip por su nombre
    private AudioClip GetAudioClipByName(string name)
    {
        foreach (var clip in audioClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
}
