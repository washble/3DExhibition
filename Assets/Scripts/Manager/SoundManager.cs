using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    private AudioClip tempDummyAudioClip;
    
    private void PlaySound(AudioSource audioSource, AudioClip audioClip, bool oneShot = false)
    {
        if (!oneShot)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    
    private void PlayStop(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    
    public void PlayBackgroundSound()
    {
        PlaySound(backgroundAudioSource, tempDummyAudioClip);
    }
    
    public void StopBackgroundSound()
    {
        PlayStop(backgroundAudioSource);
    }
    
    public void PlayEffectSound()
    {
        PlaySound(backgroundAudioSource, tempDummyAudioClip, true);
    }
    
    public void StopEffectSound()
    {
        PlayStop(backgroundAudioSource);
    }
}
