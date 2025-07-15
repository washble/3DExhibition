using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OpenURLDetector : MonoBehaviour
{
    [SerializeField] private string url;

    [TextArea][SerializeField] private string urlTile;

    private void Awake()
    {
        if (TryGetComponent(out Collider thisCollider))
        {
            thisCollider.isTrigger = true;
        }
        else
        {
            enabled = false;
        }
    }

    private void ShowOpenURL()
    {
        URLDisplayGroup.Instance.ShowUrlDisplay(url, urlTile);
    }

    private void HideOpenURL()
    {
        URLDisplayGroup.Instance.HideUrlDisplay();
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
