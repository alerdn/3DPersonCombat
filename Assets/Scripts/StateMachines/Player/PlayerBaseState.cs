using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 force = stateMachine.ForceReceiver.Movement;
        stateMachine.CharacterController.Move((motion + force) * deltaTime);
    }

    protected Vector3 CalculateFreelookMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    protected Vector3 CalculateTargetingMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;

        return movement;
    }

    protected void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }

    protected void FaceTarget()
    {
        if (!stateMachine.Targeter.CurrentTarget) return;

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    protected void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    protected void OnDodge()
    {
        if (!stateMachine.Stamina.TryUseStamina(stateMachine.DodgeStaminaCost)) return;

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(new PlayerDodgingState(stateMachine, Vector2.down));
        }
        else
        {
            stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
        }
    }

    protected void OnUseItem()
    {
        stateMachine.Inventory.UseItem();
    }

    protected bool CanAttack(int attackIndex, bool isJumping = false)
    {
        if (stateMachine.CurrentWeapon is MeleeWeapon weapon)
        {
            if (weapon.Attacks.Length <= attackIndex) return false;

            float staminaCost = GetAttackStaminaCost(attackIndex);
            return stateMachine.Stamina.TryUseStamina(staminaCost);
        }
        else
        {
            if (isJumping) return false;

            float manaCost = stateMachine.Spellbook.CurrentSpell.ManaCost;
            return stateMachine.Mana.TryUseMana(manaCost);
        }
    }

    protected void EnterAttackingState(int attackIndex, bool isJumping = false)
    {
        if (stateMachine.CurrentWeapon is Staff)
        {
            if (isJumping) return;

            stateMachine.SwitchState(new PlayerCastingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attackIndex, isJumping));
        }
    }

    protected void HandleLedgeDetect(Vector3 ledgeForward, Vector3 closestPoint)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, closestPoint));
    }

    protected float GetAttackStaminaCost(int attackIndex)
    {
        MeleeWeapon weapon = stateMachine.CurrentWeapon as MeleeWeapon;
        float staminaMultiplier = weapon.Attacks[attackIndex].StaminaMultiplier;
        return stateMachine.CurrentWeapon.StaminaCost * staminaMultiplier;
    }
}
