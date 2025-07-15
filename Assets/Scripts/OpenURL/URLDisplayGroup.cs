using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class URLDisplayGroup : Singleton<URLDisplayGroup>
{
    [SerializeField] private Button urlDof;
    [SerializeField] private TextMeshProUGUI urlTitleTMP;
    [SerializeField] private Button urlOpenButton;

    [Space(10)]
    [SerializeField] private GameObject childGroup;
    
    private string url;

    private void OnEnable()
    {
        urlDof.onClick.AddListener(ClickedDof);
        urlOpenButton.onClick.AddListener(UrlOpenHandler);
    }

    private void OnDisable()
    {
        urlDof.onClick.RemoveListener(ClickedDof);
        urlOpenButton.onClick.RemoveListener(UrlOpenHandler);
    }
    
    private void ClickedDof()
    {
        HideUrlDisplay();
    }

    private void UrlOpenHandler()
    {
        Application.OpenURL(url);
        HideUrlDisplay();
    }

    public void ShowUrlDisplay(string targetUrl, string urlTitle)
    {
        urlTitleTMP.text = urlTitle;
        url = targetUrl;
        
        childGroup.gameObject.SetActive(true);
    }

    public void HideUrlDisplay()
    {
        urlTitleTMP.text = string.Empty;
        url = string.Empty;
        
        childGroup.gameObject.SetActive(false);
    }
}
