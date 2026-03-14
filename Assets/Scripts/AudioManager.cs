using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip tap;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        SFXSource.PlayOneShot(clip, volume);
    }

    public void PlaySFXIfNotPlaying(AudioClip clip)
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.clip = clip;
            SFXSource.Play();
        }
    }
}
