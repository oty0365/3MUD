using UnityEngine;

public class Coin : Objectile
{
    public int amount;
    public override void OnHit()
    {
        PlayerStatus.Instance.CoinConsume(amount);
        Death();
    }
}
