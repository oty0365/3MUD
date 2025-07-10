using UnityEngine;

public class CollisionDamageObject : CollisionObj
{
    [SerializeField] private float damage;

    protected override void OnCollision()
    {
        PlayerStatus.Instance.TakeDamage(damage);
    }
}
