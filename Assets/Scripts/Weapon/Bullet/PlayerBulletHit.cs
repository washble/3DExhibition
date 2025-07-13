using UnityEngine;
using UnityEngine.VFX;

public class PlayerBulletHit : MonoBehaviour
{
    [SerializeField] private VisualEffect hitVFX;

    private void Awake()
    {
        if(!hitVFX) { gameObject.TryGetComponent(out hitVFX); }
    }

    private void OnEnable()
    {
        hitVFX.Play();
    }

    private void OnDisable()
    {
        hitVFX.Stop();
    }
}
