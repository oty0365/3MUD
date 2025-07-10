using UnityEngine;

public class CollisionCoinObject : CollisionObj
{
    [SerializeField] private int coinAmount;

    protected override void OnCollision()
    {
        PlayerStatus.Instance.CoinConsume(coinAmount);
        ObjectPooler.Instance.Return(gameObject);
    }
}
