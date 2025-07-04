using System;
using System.Collections;
using UnityEngine;

public class PlayerStatus : HalfSingleMono<PlayerStatus>
{
    //public
    public event Action<PlayerEffects, float> playerInteractEffects;
    public event Action<float> blinkEffect;
    public Action<float> onHpChangeUI;
    public Action<float> onAtkChangeUI;
    public Action<float> onDefChangeUI;
    public Action<float> onSpdChangeUI;
    public Action<float> onWipChangeUI;
    public Action<float> onMaxHpChangeUI;
    public Action<int> onCoinChangeUI;
    public event Action onPlayerDeath;
    //private
    private bool _isAlive;
    private bool _isInfinate;
    private float _playerInfiateTime;
    private float _playerMaxHp;
    private float _playerCurHp;
    private int _currentRunCoinCount;
    private float _currentRunDeffence;
    private float _currentRunAttack;
    private float _currentRunSpeed;
    private float _currentRunWill;

    private Coroutine _currentInfinateTimeFlow;
    //get-set
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
        }
    }
    public bool IsInfinate
    {
        get => _isInfinate;
        set
        {
            _isInfinate = value;
        }
    }
    public float PlayerInfinateTime
    {
        get => _playerInfiateTime;
        set
        {
            _playerInfiateTime = value;
        }
    }

    public float PlayerMaxHp
    {
        get => _playerMaxHp;
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            if (value < PlayerCurHp)
            {
                PlayerCurHp = value;
            }
            if (value != _playerMaxHp)
            {
                _playerMaxHp = value;
                onMaxHpChangeUI?.Invoke(value);
            }
        }
    }

    public float CurrentRunDeffence
    {
        get => _currentRunDeffence;
        set
        {
            _currentRunDeffence = value;
            onDefChangeUI?.Invoke(_currentRunDeffence);
        }
    }
    public float CurrentRunAttack
    {
        get => _currentRunAttack;
        set
        {
            if (value <= 0)
            {
                _currentRunAttack = 0;
            }
            else
            {
                _currentRunAttack = value;
            }
            onAtkChangeUI?.Invoke(_currentRunAttack);
        }
    }
    public float CurrentRunSpeed
    {
        get => _currentRunSpeed;
        set
        {
            if (value <= 0)
            {
                _currentRunSpeed = 0;
            }
            else
            {
                _currentRunSpeed = value;
            }
            onSpdChangeUI?.Invoke(_currentRunSpeed);

        }
    }
    public float CurrentRunWill
    {
        get => _currentRunWill;
        set
        {
            _currentRunWill = value;
            onWipChangeUI?.Invoke(_currentRunSpeed);
        }
    }
    public int CurrentRunCoinCount
    {
        get => _currentRunCoinCount;
        set
        {
            _currentRunCoinCount = value;
            onCoinChangeUI?.Invoke(_currentRunCoinCount);
        }
    }
    public float PlayerCurHp
    {
        get => _playerCurHp;
        set
        {
            if (value > PlayerMaxHp)
            {
                value = PlayerMaxHp;
            }
            if (value <= 0)
            {
                value = 0;
                onPlayerDeath?.Invoke();
            }
            if (value != _playerCurHp)
            {
                _playerCurHp = value;
                onHpChangeUI?.Invoke(value);
            }
        }
    }
    //func-public
    public void TakeDamage(float amount)
    {
        if (!IsInfinate)
        {
            PlayerCurHp -= amount;
            if (PlayerCurHp > 0)
            {
                SetInfinateTime(PlayerInfinateTime);
                playerInteractEffects?.Invoke(PlayerEffects.Hit, amount);
                blinkEffect?.Invoke(PlayerInfinateTime);
            }
        }

    }
    public void CoinConsume(int amount)
    {
        CurrentRunCoinCount += amount;
        playerInteractEffects?.Invoke(PlayerEffects.CoinConsume, amount);
    }
    public void Heal(float amount)
    {
        PlayerCurHp += amount;
        playerInteractEffects?.Invoke(PlayerEffects.Heal, amount);
    }
    public void SetMaxHp(float amount)
    {
        PlayerMaxHp += amount;
    }
    public void SetInfinateTime(float time)
    {
        if (_currentInfinateTimeFlow == null)
        {
            _currentInfinateTimeFlow = StartCoroutine(InfinateTimeFlow(time));
        }
        
    }
    //func-private
    private IEnumerator InfinateTimeFlow(float time)
    {
        IsInfinate = true;
        yield return new WaitForSeconds(time);
        IsInfinate = false;
        _currentInfinateTimeFlow = null;
    }
    private void InitializeStatus()
    {
        PlayerMaxHp = 30f;
        PlayerCurHp = PlayerMaxHp;
        IsAlive = true;
        IsInfinate = false;
        PlayerInfinateTime = 1f;
    }
    
    //lifefunc-private
    //lifefunc-protected
    protected override void Awake()
    {
        base.Awake();
        InitializeStatus();
    }
}
