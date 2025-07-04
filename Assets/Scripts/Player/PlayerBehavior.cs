using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum PlayerBehave
{
    Run = 0,
    Idle,
    Jump,
    Land,
    Slide,
    Death
}

public class PlayerBehavior : HalfSingleMono<PlayerBehavior>
{
    public event Action<PlayerBehave> setColider;
    private Animator _ani;
    private PlayerBehave _playerState;
    public PlayerBehave PlayerState
    {
        get => _playerState;
        set
        {
            if (value != _playerState)
            {
                AnimationController(value);
                _playerState = value;
                setColider?.Invoke(PlayerState);
            }
        }
    }
    public void ChangeState(PlayerBehave newState)
    {
        if (PlayerState != PlayerBehave.Death)
        {
            PlayerState = newState;
        }
    }
    public void ForceChangeToIdle()
    {
        PlayerState = PlayerBehave.Idle;
    }
    public bool CheckBehave(PlayerBehave behave)
    {
        if (PlayerState == behave)
        {
            return true;
        }
        return false;
    }
    private void AnimationController(PlayerBehave newState)
    {
        if (newState == PlayerBehave.Death)
        {
            _ani.SetTrigger("Death");
        }
        else
        {
            HandleStateTransition(newState);
        }

        _playerState = newState;
    }

    private void HandleStateTransition(PlayerBehave newState)
    {
        bool wasIdle = (_playerState == PlayerBehave.Idle);
        bool willBeIdle = (newState == PlayerBehave.Idle);

        if (wasIdle && !willBeIdle)
        {
            _ani.SetTrigger("Start");
        }
        else if (!wasIdle && willBeIdle)
        {
            _ani.SetTrigger("Return");
        }
        _ani.SetInteger("Behavior", (int)newState);
    }
    private void Start()
    {
        PlayerMove.Instance.checkCurrentState +=CheckBehave;
        PlayerMove.Instance.setState += ChangeState;
        GameFlowManager.Instance.startIdel += ForceChangeToIdle;
    }
    protected override void Awake()
    {
        _ani = GetComponent<Animator>();
        PlayerState = PlayerBehave.Idle;
    }
}

