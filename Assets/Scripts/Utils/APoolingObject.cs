using UnityEngine;
public interface IPoolingObject
{
    public PoolObjectType ObjectType { get; set; }
    void OnBirth();
    void OnDeathInit();
}