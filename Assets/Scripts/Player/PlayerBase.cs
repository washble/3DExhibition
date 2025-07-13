using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerWeaponController))]
[RequireComponent(typeof(PlayerEffectController))]
public class PlayerBase : Singleton<PlayerBase>
{
    protected InputManager inputManager;
    protected UIManager uiManager;
    
    internal NavMeshAgent navMeshAgent;
    
    internal PlayerWeaponController playerWeaponController;
    internal PlayerEffectController playerEffectController;
    
    protected override void Awake()
    {
        base.Awake();
        
        inputManager = InputManager.Instance;
        uiManager = UIManager.Instance;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        playerWeaponController = GetComponent<PlayerWeaponController>();
        playerEffectController = GetComponent<PlayerEffectController>();
    }
}
