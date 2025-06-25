using System;
using System.Collections;
using UnityEngine;

public abstract class Objectile : APoolingObject
{
    public static float moveSpeed;
    public static Action<float> changeMoveSpeed;

    public float localMoveSpeed;
    public FloatRange spawnTime;
    public APoolingObject colideParticle;


    [SerializeField] private float localHeight;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody2D _rb2D;

    public override void OnBirth()
    {
        OnInit();
    }

    public override void OnDeathInit()
    {
        changeMoveSpeed -= ChangeAllMoveSpeed;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (colideParticle != null)
            {
                ObjectPooler.Instance.Get(colideParticle, collision.transform.position, Vector3.zero);
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
