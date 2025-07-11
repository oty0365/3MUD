using System.Collections;
using UnityEngine;

public class RockObjstacle : Objectile
{
    [SerializeField] private EventObj eventObj;

    public override void OnBirth()
    {
        base.OnBirth();
        eventObj.target = PlayerStatus.Instance.gameObject;
        StartCoroutine(CheckDistanceFlow());
    }

    private IEnumerator CheckDistanceFlow()
    {
        while (true)
        {
            if (!eventObj.CheckDistance(eventObj.target, gameObject, eventObj.distance))
            {
                ObjectPooler.Instance.Return(gameObject);
                yield break;
            }
            yield return new WaitForSeconds(eventObj.checkTime);
        }
    }
}
