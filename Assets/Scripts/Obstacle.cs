using System;
using System.Collections;
using UnityEngine;

public abstract class Obstacle : APoolingObject
{
    public static float moveSpeed;
    public static Action changeMoveSpeed;

    public float localMoveSpeed;

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
            OnHit();
        }
    }

    public virtual void OnHit()
    {
        OnHitEffect();
        PlayerInfo.Instance.TakeDamage(damage);
    }
    public virtual void OnHitEffect()
    {
        UIManager.Instance.ImoPanel.text = "-" + damage;
        StartCoroutine(HitFlow());
    }
    private IEnumerator HitFlow()
    {
        var color = UIManager.Instance.ImoPanel.color;
        color = Color.clear;
        while (Mathf.Abs(color.a - Color.red.a) < 0.02)
        {
            color = Color.Lerp(color, Color.red, Time.deltaTime * 10f);
        }
        yield return new WaitForSeconds(0.1f);
        while (Mathf.Abs(color.a - Color.clear.a) < 0.02)
        {
            color = Color.Lerp(color, Color.clear, Time.deltaTime * 10f);
        }
    }

    public virtual void OnInit()
    {
        changeMoveSpeed -= ChangeAllMoveSpeed;
        changeMoveSpeed += ChangeAllMoveSpeed;

        _rb2D.linearVelocity = new Vector2(-localMoveSpeed * moveSpeed, 0);

        Vector3 local = transform.localPosition;
        transform.localPosition = new Vector3(local.x, localHeight, local.z);
    }

    public void ChangeAllMoveSpeed()
    {
        _rb2D.linearVelocity = new Vector2(-localMoveSpeed * moveSpeed, 0);
    }
}
