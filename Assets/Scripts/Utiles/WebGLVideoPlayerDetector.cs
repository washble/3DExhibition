using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class WebGLVideoPlayerDetector : MonoBehaviour
{
    [SerializeField] private string videoFileName;

    [Header("[Trigger]")] 
    [SerializeField] private LayerMask layerMask;

    private VideoPlayer videoPlayer;
    private string videoPath;

    private void Awake()
    {
        if (!TryGetComponent(out videoPlayer)) { enabled = true; }
        
        videoPath = Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
    }

    private void PlayVideo()
    {
        videoPlayer.Play();
    }
    
    private void StopVideo()
    {
        videoPlayer.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            PlayVideo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            StopVideo();
        }
    }
}
