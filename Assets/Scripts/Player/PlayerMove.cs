using System;
using UnityEngine;

[RequireComponent(typeof(PlayerBehavior))]
public class PlayerMove : HalfSingleMono<PlayerMove>
{
    public event Action<PlayerBehave> setState;
    public event Func<PlayerBehave,bool> checkCurrentState;

    private Rigidbody2D _rb2D;
    private float _currentHeight;

    public float CurrentHeight
    {
        get => _currentHeight;
        set
        {
            if (Mathf.Abs(value - _currentHeight) > 0.01f && ((bool)checkCurrentState?.Invoke(PlayerBehave.Jump) || (bool)checkCurrentState?.Invoke(PlayerBehave.Land)))
            {
                if (value > _currentHeight)
                {
                    setState?.Invoke(PlayerBehave.Jump);
                }
                else
                {
                    setState?.Invoke(PlayerBehave.Land);
                }
                _currentHeight = value;
            }
        }
    }



 

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        PlayerInteraction.Instance.setOnGround += GroundCheck;
        _currentHeight = _rb2D.position.y;
    }

    void Update()
    {
        CurrentHeight = _rb2D.position.y;
    }

    public void Jump()
    {
        var result = checkCurrentState?.Invoke(PlayerBehave.Run);
        if ((bool)result)
        {
            _rb2D.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            setState?.Invoke(PlayerBehave.Jump);
        }
    }

    public void StartSlide()
    {
        var result = checkCurrentState?.Invoke(PlayerBehave.Run);
        if ((bool)result)
        {
            setState?.Invoke(PlayerBehave.Slide);
        }
    }

    public void EndSlide()
    {
        var result = checkCurrentState?.Invoke(PlayerBehave.Slide);
        if ((bool)result)
        {
            setState?.Invoke(PlayerBehave.Run);
        }
    }
    public void GroundCheck()
    {
        if (!(bool)checkCurrentState?.Invoke(PlayerBehave.Idle) && !(bool)checkCurrentState?.Invoke(PlayerBehave.Slide))
        {
            setState?.Invoke(PlayerBehave.Run);
        }
    }

}
