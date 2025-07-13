using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerBulletVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect bulletVFX;
    [SerializeField] private TrailRenderer trailRenderer;
    
    [SerializeField] private AnimationCurve sizeAnimationCurve;
    [SerializeField] private float sizeSpeed = 1;

    private const string Size = "Orb Size";

    private void Awake()
    {
        if(!bulletVFX) { bulletVFX = GetComponent<VisualEffect>(); }
        if(!trailRenderer) { trailRenderer = bulletVFX.GetComponentInChildren<TrailRenderer>(); }
    }

    private void OnEnable()
    {
        trailRenderer.enabled = true;
        LifetimeSize().Forget();
    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
    }

    private async UniTaskVoid LifetimeSize()
    {
        float time = 0;
        
        while (time < 1 && gameObject.activeInHierarchy)
        {
            time += Time.deltaTime * sizeSpeed;
            float size = sizeAnimationCurve.Evaluate(time);
            bulletVFX.SetFloat(Size, size);

            await UniTask.Yield(destroyCancellationToken);
        }
    }
}
