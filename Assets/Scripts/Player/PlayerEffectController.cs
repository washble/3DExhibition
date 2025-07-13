using UnityEngine;
using UnityEngine.VFX;

public class PlayerEffectController : MonoBehaviour
{
    [Header("[Position]")]
    [SerializeField] private Transform runSmokeTransform;
    
    private EffectSpawner effectSpawner;

    private VisualEffect runSmokeEffect;
    
    private void Start()
    {
        effectSpawner = EffectSpawner.Instance;

        Init();
    }

    private void Init()
    {
        GameObject runSmoke = effectSpawner.Get(EffectType.RunSmoke);
        runSmokeEffect = runSmoke.GetComponent<VisualEffect>();
        runSmokeEffect.Stop();
    }
    
    public void RunSmokePlay()
    {
        runSmokeEffect.transform.SetPositionAndRotation(runSmokeTransform.position, runSmokeTransform.rotation);
        runSmokeEffect.transform.SetParent(runSmokeTransform, true);
        runSmokeEffect.Play();
    }

    public void RunSmokeStop()
    {
        if(!runSmokeEffect) { return; }
        
        runSmokeEffect.transform.SetParent(null, true);
        runSmokeEffect.Stop();
    }
}
