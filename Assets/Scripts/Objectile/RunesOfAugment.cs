using System;
using System.Collections;
using UnityEngine;

public class RunesOfAugment : Objectile
{
    [SerializeField] private float checkTime;
    [SerializeField] private float distance;
    public event Action onAugmentActivate;
    private GameObject player;
    public override void OnBirth()
    {
        base.OnBirth();
        onAugmentActivate += UIManager.Instance.AugmentSelection;
        player = PlayerStatus.Instance.gameObject;
        StartCoroutine(CheckDistanceFlow());
    }

    public override void OnDeathInit()
    {
        onAugmentActivate -= UIManager.Instance.AugmentSelection;
        base.OnDeathInit();
    }

    private IEnumerator CheckDistanceFlow()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distance)
            {
                onAugmentActivate?.Invoke();
                yield return new WaitForSeconds(0.8f);
                ObjectPooler.Instance.Return(gameObject);
            }
            yield return new WaitForSeconds(checkTime);
        }
    }
    
}
