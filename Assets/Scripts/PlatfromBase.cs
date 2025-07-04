using System;
using UnityEngine;

public abstract class PlatformBase : APoolingObject
{
    public static float moveSpeed;
    public static Action<float>changeMoveSpeed;
    [SerializeField] protected float localMoveSpeed;
    [SerializeField] private float returnDistance;
    [SerializeField] protected GameObject endOfThisMapObj;
    private GameObject _target;
    public GameObject currentMap;
    protected Rigidbody2D _rb2D;
    private void Awake()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        _rb2D.linearVelocity = new Vector2(-(1 * localMoveSpeed*moveSpeed), 0);
        changeMoveSpeed += ChangeAllMoveSpeed;
    }
    protected virtual void Start()
    {
        _target = PlayerStatus.Instance.gameObject;
    }
    protected virtual void Update()
    {
        CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        if (Vector2.Distance(_target.transform.position, gameObject.transform.position) > returnDistance)
        {
            Death();
        }
    }

    public override void OnBirth()
    {
        _rb2D.linearVelocity = new Vector2(-(1 * localMoveSpeed*moveSpeed), 0);
        endOfThisMapObj.SetActive(true);
    }
    public override void OnDeathInit()
    {
        
    }
    public void ChangeAllMoveSpeed(float speed)
    {
        moveSpeed = speed;
        _rb2D.linearVelocity = new Vector2(-(1 * localMoveSpeed*moveSpeed), 0);
    }
}
