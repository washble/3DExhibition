using UnityEngine;

public class OpenURLDetector : MonoBehaviour
{
    [SerializeField] private readonly string url;

    private void ShowOpenURL()
    {
        
    }

    private void HideOpenURL()
    {
        
    }

    private void OpenURL()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)GameObjectLayer.Player)
        {
            ShowOpenURL();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (int)GameObjectLayer.Player)
        {
            HideOpenURL();
        }
    }
}
