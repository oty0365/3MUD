using UnityEngine;

public enum PlayerBehave
{
    Run = 0,
    Idle,
    Jump,
    Land,
    Slide
}

public class PlayerMove : HalfSingleMono<PlayerMove>
{
    private Rigidbody2D _rb2D;
    private Animator _ani;
    private PlayerBehave _playerState;
    private float _currentHeight;

    public float CurrentHeight
    {
        get => _currentHeight;
        set
        {
            if (Mathf.Abs(value - _currentHeight) > 0.01f &&
                (_playerState == PlayerBehave.Jump || _playerState == PlayerBehave.Land))
            {
                if (value > _currentHeight)
                {
                    ChangeState(PlayerBehave.Jump);
                }
                else
                {
                    ChangeState(PlayerBehave.Land);
                }
                _currentHeight = value;
            }
        }
    }

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

    private void AnimationController()
    {
        switch (PlayerState)
        {
            case PlayerBehave.Run:
                _ani.Play("PlayerRun");
                break;
            case PlayerBehave.Land:
                _ani.Play("PlayerLand");
                break;
            case PlayerBehave.Jump:
                _ani.Play("PlayerJump");
                break;
            case PlayerBehave.Idle:
                _ani.Play("PlayerIdle");
                break;
            case PlayerBehave.Slide:
                _ani.Play("PlayerSlide");
                break;
        }
    }

    void Start()
    {
        _ani = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        PlayerState = PlayerBehave.Idle;
        _currentHeight = _rb2D.position.y;
    }

    void FixedUpdate()
    {
        CurrentHeight = _rb2D.position.y;
    }

    public void Jump()
    {
        if (PlayerState == PlayerBehave.Run)
        {
            _rb2D.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            ChangeState(PlayerBehave.Jump);
        }
    }

    public void StartSlide()
    {
        if (PlayerState == PlayerBehave.Run)
        {
            ChangeState(PlayerBehave.Slide);
        }
    }

    public void EndSlide()
    {
        if (PlayerState == PlayerBehave.Slide)
        {
            ChangeState(PlayerBehave.Run);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && PlayerState != PlayerBehave.Idle)
        {
            ChangeState(PlayerBehave.Run);
        }
    }

    public void ChangeState(PlayerBehave newState)
    {
        PlayerState = newState;
    }
}
