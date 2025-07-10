using UnityEngine;

public class CollisionHealObject : CollisionObj
{
    [SerializeField] private float healAmount;

    protected override void OnCollision()
    {
        PlayerStatus.Instance.Heal(healAmount);
        ObjectPooler.Instance.Return(gameObject);
    }
}
