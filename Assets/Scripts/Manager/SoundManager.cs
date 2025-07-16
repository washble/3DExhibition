using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    private void PlaySound(AudioSource audioSource, AudioClip audioClip = null, bool oneShot = false)
    {
        if (!oneShot)
        {
            if(audioClip is not null) { audioSource.clip = audioClip; }
            
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void PauseSound(AudioSource audioSource)
    {
        audioSource.Pause();
    }
    
    private void PlayStop(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    
    public void PlayBackgroundSound()
    {
        PlaySound(backgroundAudioSource);
    }

    public void PauseBackgroundSound()
    {
        PauseSound(backgroundAudioSource);
    }
    
    public void StopBackgroundSound()
    {
        PlayStop(backgroundAudioSource);
    }
    
    public void PlayEffectSound(AudioClip audioClip, Vector3 position)
    {
        effectAudioSource.transform.position = position;
        PlaySound(effectAudioSource, audioClip, true);
    }
    
    public void StopEffectSound()
    {
        PlayStop(backgroundAudioSource);
    }
}
