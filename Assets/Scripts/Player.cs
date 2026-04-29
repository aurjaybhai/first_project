using UnityEngine;

public class Player : Entity
{
    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
    }
}
