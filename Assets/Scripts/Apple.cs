using UnityEngine;
using UnityEngine.Rendering;

public class Apple: Objectile
{
    public int amount;
    
    public override void OnHit()
    {
        PlayerStatus.Instance.Heal(amount);
        ObjectPooler.Instance.Return(gameObject);
    }
}
