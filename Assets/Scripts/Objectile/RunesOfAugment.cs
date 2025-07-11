using System;
using System.Collections;
using UnityEngine;

public class RunesOfAugment : Objectile
{
    [SerializeField] private EventObj eventObj;
    public event Action onAugmentActivate;
    public override void OnBirth()
    {
        base.OnBirth();
        eventObj.target = PlayerStatus.Instance.gameObject;
        onAugmentActivate += UIManager.Instance.AugmentSelection;
        StartCoroutine(CheckDistanceFlow());
    }

    private IEnumerator CheckDistanceFlow()
    {
        while (true)
        {
            if (eventObj.CheckDistance(eventObj.target, gameObject, eventObj.distance))
            {
                onAugmentActivate?.Invoke();
                yield return new WaitForSeconds(0.8f);
                ObjectPooler.Instance.Return(gameObject);
                yield break;
            }
            yield return new WaitForSeconds(eventObj.checkTime);
        }
    }
    public override void OnDeathInit()
    {
        onAugmentActivate -= UIManager.Instance.AugmentSelection;
        base.OnDeathInit();
    }

    
}
