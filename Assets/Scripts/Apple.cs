using UnityEngine;

public class Apple: Objectile
{
    public int amount;
    public override void OnHit()
    {
        PlayerStatus.Instance.Heal(amount);
        Death();
    }
}
