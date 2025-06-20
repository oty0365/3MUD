using UnityEngine;

public class Coin : Objectile
{
    public int amount;
    public override void OnHit()
    {
        PlayerInfo.Instance.CoinConsume(amount);
        Death();
    }
}
