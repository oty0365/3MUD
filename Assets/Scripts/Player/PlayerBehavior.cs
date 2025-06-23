using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
    private Animator _ani;
    private PlayerBehave _playerState;
    public PlayerBehave PlayerState
    {
        get => _playerState;
        set
        {
            if (value != _playerState)
            {
                _playerState = value;
                AnimationController();
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
    public bool CheckBehave(PlayerBehave behave)
    {
        if (PlayerState == behave)
        {
            return true;
        }
        return false;
    }
    private void AnimationController()
    {
        _ani.SetInteger("Behavior",(int)PlayerState);
    }
    private void Start()
    {
        PlayerMove.Instance.checkCurrentState +=CheckBehave;
        PlayerMove.Instance.setState += ChangeState;
    }
    protected override void Awake()
    {
        _ani = GetComponent<Animator>();
        PlayerState = PlayerBehave.Idle;
    }
}

