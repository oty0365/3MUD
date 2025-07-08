using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Objectile : MonoBehaviour, IPoolingObject
{
    
    public static float moveSpeed;
    public static Action<float> changeMoveSpeed;
    public float localMoveSpeed;
    public FloatRange spawnTime;
    public GameObject collideParticle;
    public PoolObjectType ObjectType{get=>objectType;set{}}
    [SerializeField] private PoolObjectType objectType;
    [SerializeField] private float localHeight;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody2D _rb2D;

    public virtual void OnBirth()
    {
        OnInit();
    }

    public void OnDeathInit()
    {
        changeMoveSpeed -= ChangeAllMoveSpeed;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collideParticle != null)
            {
                ObjectPooler.Instance.Get(collideParticle, collision.transform.position, Vector3.zero);
            }

            OnHit();
        }
    }

    public virtual void OnHit()
    {
        PlayerStatus.Instance.TakeDamage(damage);
    }

    public virtual void OnInit()
    {
        ObjectType = objectType;
        changeMoveSpeed -= ChangeAllMoveSpeed;
        changeMoveSpeed += ChangeAllMoveSpeed;

        _rb2D.linearVelocity = new Vector2(-localMoveSpeed * moveSpeed, 0);

        Vector3 local = transform.localPosition;
        transform.localPosition = new Vector3(local.x, localHeight, local.z);
    }

    public void ChangeAllMoveSpeed(float amount)
    {
        moveSpeed = amount;
        _rb2D.linearVelocity = new Vector2(-localMoveSpeed * moveSpeed, 0);
    }
    
}
