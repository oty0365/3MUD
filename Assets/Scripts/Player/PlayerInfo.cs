using System.Collections;
using UnityEngine;

public class PlayerInfo : HalfSingleMono<PlayerInfo>
{
    public bool isAlive;
    public bool isInfinate;
    public float playerInfiateTime;
    public float playerMaxHp;
    private SpriteRenderer _sr;
    private float _playerCurHp;
    private Coroutine _currentHitFlow;
    private Coroutine _currentInfiateTimeFlow;
    private int _currentRunCoinCount;
    private float _currentRunDeffence;
    private float _currentRunAttack;
    private float CurrentRunDeffence
    {
        get => _currentRunDeffence;
        set
        {
            _currentRunDeffence = value;
            //UIManager.Instance.statusModal.deffendTmp.text = _currentRunDeffence.ToString();
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
            //UIManager.Instance.statusModal.attackTmp.text = _currentRunAttack.ToString();
        }
    }
    public int CurrentRunCoinCount
    {
        get => _currentRunCoinCount;
        set
        {
            _currentRunCoinCount = value;
        }
    }
    public float PlayerCurHp
    {
        get => _playerCurHp;
        set
        {
            if (value > playerMaxHp)
            {
                value = playerMaxHp;
            }
            if (value <= 0)
            {
                value = 0;
                GameFlowManager.Instance.EndGame();
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
        isInfinate = false;
        isAlive = true;
        UIManager.Instance.hpBarUI.maxValue = playerMaxHp;
        PlayerCurHp = playerMaxHp;
        _sr = gameObject.GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        if (!isInfinate)
        {
            PlayerCurHp -= damage;
            OnHitEffect(damage);
        }

    }
    public void CoinConsume(int coin)
    {
        CurrentRunCoinCount += 1;
        OnCoinEffect(coin);

    }
    private void OnCoinEffect(int coin)
    {
        if (coin > 0)
        {
            UIManager.Instance.ImoPanel.text = "+"+coin;
        }
        else
        {
            UIManager.Instance.ImoPanel.text = "-" + coin;
        }
        if (_currentHitFlow != null)
        {
            StopCoroutine(_currentHitFlow);
        }
        _currentHitFlow = StartCoroutine(HitFlow(Color.yellow));


    }
    private void OnHitEffect(float damage)
    {
        if (PlayerCurHp != 0)
        {
            UIManager.Instance.ImoPanel.text = "-" + damage;
            if (_currentHitFlow != null)
            {
                StopCoroutine(_currentHitFlow);
            }
            _currentHitFlow = StartCoroutine(HitFlow(Color.red));
            if (_currentInfiateTimeFlow == null)
            {
                _currentInfiateTimeFlow = StartCoroutine(InfinateTimeFlow());
            }
        }
    }
    private IEnumerator HitFlow(Color targetColor)
    {
        var panel = UIManager.Instance.ImoPanel;
        panel.color = Color.clear;
        while (Mathf.Abs(panel.color.a - targetColor.a) > 0.02f)
        {
            panel.color = Color.Lerp(panel.color, targetColor, Time.deltaTime * 10f);
            yield return null;
        }
        yield return new WaitForSeconds(0.17f);
        while (Mathf.Abs(panel.color.a - Color.clear.a) > 0.02f)
        {
            panel.color = Color.Lerp(panel.color, Color.clear, Time.deltaTime * 10f);
            yield return null;
        }
    }
    private IEnumerator InfinateTimeFlow()
    {
        isInfinate = true;
        var section = playerInfiateTime / 8;
        var counter = 1;
        for(var i = 0f; i <= playerInfiateTime; i += Time.deltaTime)
        {
            if (i > counter * section)
            {
                counter++;
                _sr.enabled = !_sr.enabled;
            }
            yield return null;
        }
        _sr.enabled = true;
        yield return new WaitForSeconds(playerInfiateTime);
        isInfinate = false;
        _currentInfiateTimeFlow = null;
    }
}
