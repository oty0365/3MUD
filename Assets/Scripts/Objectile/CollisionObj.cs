using System;
using UnityEngine;

public abstract class CollisionObj : MonoBehaviour
{
    protected GameObject _collideParticle;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_collideParticle != null)
            {
                ObjectPooler.Instance.Get(_collideParticle, collision.transform.position, Vector3.zero);
            }
            OnCollision();
        }
    }

    protected void Start()
    {
        if (gameObject.TryGetComponent<CollisionEffect>(out var collisionEffect))
        {
            _collideParticle = collisionEffect.collideParticle;
        }
    }

    protected abstract void OnCollision();
}
