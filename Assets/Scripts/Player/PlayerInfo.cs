using UnityEngine;

public class PlayerInfo : HalfSingleMono<PlayerInfo>
{
    public bool isAlive;
    public float playerMaxHp;
    private float _playerCurHp;
    public float PlayerCurHp
    {
        get => _playerCurHp;
        set
        {
            if (value > playerMaxHp)
            {
                value = playerMaxHp;
            }
            if (value < 0)
            {
                value = 0;
                GameFlowManager.Instance.StopGame();
            }
            if (value != _playerCurHp)
            {
                UIManager.Instance.hpBarUI.value = value;
                _playerCurHp = value;
            }
        }
    }
    private void Start()
    {
        isAlive = true;
        UIManager.Instance.hpBarUI.maxValue = playerMaxHp;
        PlayerCurHp = playerMaxHp;
    }
    public void TakeDamage(float damage)
    {
        PlayerCurHp -= damage;
    }
}
