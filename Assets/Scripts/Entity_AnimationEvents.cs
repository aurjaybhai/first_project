using UnityEngine;
using UnityEngine.UIElements;

public class Entity_AnimationEvents : MonoBehaviour
{

    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTargets()
    {
        entity.DamageTargets();
    }

    public void DisableMovementAndJump()
    {
        entity.EnableMovementAndJump(false);
    }

    public void EnableMovementAndJump()
    {
        entity.EnableMovementAndJump(true);
    }

    public void PlayAttackSound()
    {
        entity.PlayAttackSound();
    }


    // private void AttackStarted()
    // {
    //     // call method from player script
    //     // that method should stop movement of the player game object
    // }
}
