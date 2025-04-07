using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveRun : IMove
{
    private readonly PlayerMoveController playerMoveController;
    private readonly PlayerAnimationController playerAnimationController;
    private readonly NavMeshAgent navMeshAgent;

    private bool isSpeedRunning = false;

    public PlayerMoveRun(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
        playerAnimationController = PlayerAnimationController.Instance;
        
        navMeshAgent = playerMoveController.navMeshAgent;
    }
    
    public void Move()
    {
        playerMoveController.playerState = PlayerState.Run;
    
        Vector3 direction = playerMoveController.direction;
        Vector3 scaledMovement = ScaleMovement(direction, playerMoveController.moveType);
        Quaternion targetRotation = Quaternion.LookRotation(scaledMovement, Vector3.up);
        navMeshAgent.transform.rotation = Quaternion.Slerp
        (
            navMeshAgent.transform.rotation, 
            targetRotation, 
            Mathf.Clamp01(Time.deltaTime * playerMoveController.lookAtSpeed)
        );
        
        navMeshAgent.Move(scaledMovement);
    
        MoveAnimation();
    }

    private Vector3 ScaleMovement(Vector3 direction, PlayerMoveController.MoveType moveType)
    {
        switch (moveType)
        {
            case PlayerMoveController.MoveType.Absolute:
                return AbsoluteDirection(direction);
            case PlayerMoveController.MoveType.Relative:
                return RelativeDirection(direction);
        }

        return Vector3.zero;
    }

    private Vector3 AbsoluteDirection(Vector3 direction)
    {
        Vector3 absoluteDirection = new Vector3(direction.x, 0, direction.y).normalized;
        Vector3 scaledMovement = navMeshAgent.speed * Time.deltaTime * absoluteDirection;
        
        return scaledMovement;
    }
    
    private Vector3 RelativeDirection(Vector3 direction)
    {
        Vector3 relativeDirection 
            = navMeshAgent.transform.TransformDirection(new Vector3(direction.x, 0, direction.y)).normalized;
        Vector3 scaledMovement = navMeshAgent.speed * Time.deltaTime * relativeDirection;
        
        return scaledMovement;
    }

    public void StartSpeedRunning()
    {
        if(isSpeedRunning) { return; }

        isSpeedRunning = true;
        navMeshAgent.speed += playerMoveController.addRunSpeed;
    }

    public void StopSpeedRunning()
    {
        if(!isSpeedRunning) { return; }

        isSpeedRunning = false;
        navMeshAgent.speed -= playerMoveController.addRunSpeed;
    }

    private void MoveAnimation()
    {
        playerAnimationController.RunStart();
    }
}
