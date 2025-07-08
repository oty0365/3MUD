using System.Collections;
using UnityEngine;

public class ParticleObject : MonoBehaviour,IPoolingObject
{
    [SerializeField] PoolObjectType objectType;
    public PoolObjectType ObjectType{get=>objectType;set{}}
    public ParticleSystem prt;
    private float waitTime;


    public virtual void OnDeathInit()
    {

    }
    public virtual void OnBirth()
    {
        waitTime = prt.main.duration;
        StartCoroutine(ParticleFlow());
    }
    private IEnumerator ParticleFlow()
    {
        prt.Play();
        yield return new WaitForSeconds(waitTime);
        ObjectPooler.Instance.Return(gameObject);
    }
}
