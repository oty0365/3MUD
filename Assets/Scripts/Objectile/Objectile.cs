using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(ObjectTypeDefiner))]
public abstract class Objectile : MonoBehaviour, IPoolingObject
{
    
    public static float moveSpeed;
    public static Action<float> changeMoveSpeed;
    public float localMoveSpeed;
    public FloatRange spawnTime;
    [SerializeField] private float localHeight;
    [SerializeField] protected Rigidbody2D _rb2D;

    public virtual void OnBirth()
    {
        changeMoveSpeed -= ChangeAllMoveSpeed;
        changeMoveSpeed += ChangeAllMoveSpeed;

        _rb2D.linearVelocity = new Vector2(-localMoveSpeed * moveSpeed, 0);

        Vector3 local = transform.localPosition;
        transform.localPosition = new Vector3(local.x, localHeight, local.z);
    }

    public virtual void OnDeathInit()
    {
        changeMoveSpeed -= ChangeAllMoveSpeed;
    }
    public void ChangeAllMoveSpeed(float amount)
    {
        moveSpeed = amount;
        _rb2D.linearVelocity = new Vector2(-localMoveSpeed * moveSpeed, 0);
    }
    
}
