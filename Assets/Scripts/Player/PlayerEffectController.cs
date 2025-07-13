using UnityEngine;
using UnityEngine.VFX;

public class PlayerEffectController : MonoBehaviour
{
    [Header("[Position]")]
    [SerializeField] private Transform runSmokeTransform;
    
    private EffectSpawner effectSpawner;

    private VisualEffect runSmokeEffect = null;
    
    private void Start()
    {
        effectSpawner = EffectSpawner.Instance;

        Init();
    }

    private void Init()
    {
        GameObject runSmoke = effectSpawner.Get(EffectType.RunSmoke);
        runSmokeEffect = runSmoke.GetComponent<VisualEffect>();
        runSmokeEffect.gameObject.SetActive(false);
    }
    
    public void RunSmokePlay()
    {
        runSmokeEffect.transform.SetPositionAndRotation(runSmokeTransform.position, runSmokeTransform.rotation);
        runSmokeEffect.transform.SetParent(runSmokeTransform, true);
        runSmokeEffect.gameObject.SetActive(true);
        runSmokeEffect.Play();
    }

    public void RunSmokeStop()
    {
        runSmokeEffect.transform.SetParent(null, true);
        runSmokeEffect.gameObject.SetActive(false);
        runSmokeEffect.Stop();
    }
}
