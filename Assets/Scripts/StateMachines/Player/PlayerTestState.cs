using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3(stateMachine.InputReader.MovementValue.x, 0, stateMachine.InputReader.MovementValue.y);

        stateMachine.CharacterController.Move(stateMachine.FreeLookMovement * deltaTime * movement);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat("FreeLookSpeed", 0f, .1f, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat("FreeLookSpeed", 1f, .1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void Exit()
    {

    }
}
