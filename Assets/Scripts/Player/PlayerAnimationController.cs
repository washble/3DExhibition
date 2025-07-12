using UnityEngine;

public class PlayerAnimationController : Singleton<PlayerAnimationController>
{
    private Animator animator;

    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Walking = Animator.StringToHash("Walking");
    private readonly int Running = Animator.StringToHash("Running");
    private readonly int Attack = Animator.StringToHash("Attack");

    public AnimationSMBBase AttackSMB { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        animator = GetComponentInChildren<Animator>();
        if (!animator)
        {
            enabled = false;
        }
        
        AttackSMB = animator.GetBehaviour<AttackSMB>();
    }
    
    private void AnimationOnOff(int state)
    {
        animator.SetTrigger(state);
    }

    public void IdleStart()
    {
        AnimationOnOff(Idle);
    }
    
    public void IdleStop()
    {
        IdleAnimationOnOff(false);
    }
    
    private void IdleAnimationOnOff(bool value)
    {
        animator.SetBool(Idle, value);
    }
    
    public void WalkStart()
    {
        if (!IsWalking())
        {
            AnimationOnOff(Walking);
        }
    }
    
    public void WalkStop()
    {
        if (IsWalking())
        {
            WalkingAnimationOnOff(false);
        }
    }
    
    private bool IsWalking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Walking;
    }
    
    private void WalkingAnimationOnOff(bool value)
    {
        animator.SetBool(Walking, value);
    }
    
    public void RunStart()
    {
        if (!IsRunning())
        {
            AnimationOnOff(Running);
        }
    }

    public void RunEnd()
    {
        if (IsRunning())
        {
            RunningAnimationOnOff(false);    
        }
    }

    private bool IsRunning()
    {
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Running;
    }
    
    private void RunningAnimationOnOff(bool value)
    {
        animator.SetBool(Running, value);
    }

    public void AttackStart()
    {
        AnimationOnOff(Attack);   
    }

    public void AttackEnd()
    {
        if (IsAttack())
        {
            AttackAnimationOnOff(false);
        }
    }

    private bool IsAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Attack;
    }
    
    private void AttackAnimationOnOff(bool value)
    {
        animator.SetBool(Attack, value);
    }
}
