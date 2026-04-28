using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimationEvents : MonoBehaviour
{

    private Entity player;

    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }

    public void DamageEnemies()
    {
        player.DamageTargets();
    }

    public void DisableMovementAndJump()
    {
        player.EnableMovementAndJump(false);
    }

    public void EnableMovementAndJump()
    {
        player.EnableMovementAndJump(true);
    }

    // private void AttackStarted()
    // {
    //     // call method from player script
    //     // that method should stop movement of the player game object
    // }
}
