using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class PlatformBase : MonoBehaviour,IPoolingObject
{
    public static float moveSpeed;
    public static Action<float>changeMoveSpeed;
    [SerializeField] float checkDelay = 0.3f;
    public PoolObjectType ObjectType{get=>objectType;set{}}
    [SerializeField] private PoolObjectType objectType;
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
        StartCoroutine(CheckDistanceRoutine());
    }

    protected virtual void CheckDistance()
    {
        if (Vector2.Distance(_target.transform.position, gameObject.transform.position) > returnDistance)
        {
            ObjectPooler.Instance.Return(gameObject);
        }
    }
    private IEnumerator CheckDistanceRoutine()
    {
        while (true)
        {
            CheckDistance();
            yield return new WaitForSeconds(checkDelay);
        }
    }

    public virtual void OnBirth()
    {
        
        _rb2D.linearVelocity = new Vector2(-(1 * localMoveSpeed*moveSpeed), 0);
        endOfThisMapObj.SetActive(true);
    }
    public virtual void OnDeathInit()
    {
        
    }
    private void ChangeAllMoveSpeed(float speed)
    {
        moveSpeed = speed;
        _rb2D.linearVelocity = new Vector2(-(1 * localMoveSpeed*moveSpeed), 0);
    }
}
