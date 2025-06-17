using UnityEngine;

public enum PlayerBehave
{
    Run,
    Jump,
    Land
}

public class PlayerMove : MonoBehaviour
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
            if(value != _currentHeight && PlayerState!=PlayerBehave.Run)
            {
                if (value > CurrentHeight)
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
            if(value != _playerState)
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
        }
    }

    void Start()
    {
        _ani=gameObject.GetComponent<Animator>();
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerState = PlayerBehave.Run;

    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CurrentHeight = _rb2D.position.y;
    }
    public void Jump()
    {
        if (PlayerState==PlayerBehave.Run)
        {
            _rb2D.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
            ChangeState(PlayerBehave.Jump);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            ChangeState(PlayerBehave.Run);
        }
    }
    public void ChangeState(PlayerBehave state)
    {
        PlayerState = state;
    }
}
