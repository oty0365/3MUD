using UnityEngine;

public abstract class APoolingObject : MonoBehaviour
{

    public PoolObjectType objectType;

    public void Death()
    {
        OnDeathInit();
        ObjectPooler.Instance.Return(this);
    }
    public abstract void OnBirth();
    public abstract void OnDeathInit();
}