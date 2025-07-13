using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerBulletVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect bulletVFX;
    
    [SerializeField] private AnimationCurve sizeAnimationCurve;
    [SerializeField] private float sizeSpeed = 1;

    private const string Size = "Orb Size";

    private void Awake()
    {
        if(!bulletVFX) { bulletVFX = GetComponent<VisualEffect>(); }
    }

    private void OnEnable()
    {
        LifetimeSize().Forget();
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
