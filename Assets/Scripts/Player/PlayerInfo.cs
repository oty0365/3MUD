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
    public virtual void OnHitEffect(float damage)
    {
        if (PlayerCurHp != 0)
        {
            UIManager.Instance.ImoPanel.text = "-" + damage;
            if (_currentHitFlow != null)
            {
                StopCoroutine(_currentHitFlow);
            }
            _currentHitFlow = StartCoroutine(HitFlow());
            if (_currentInfiateTimeFlow == null)
            {
                _currentInfiateTimeFlow = StartCoroutine(InfinateTimeFlow());
            }
        }
    }
    private IEnumerator HitFlow()
    {
        var panel = UIManager.Instance.ImoPanel;
        panel.color = Color.clear;
        while (Mathf.Abs(panel.color.a - Color.red.a) > 0.02f)
        {
            panel.color = Color.Lerp(panel.color, Color.red, Time.deltaTime * 10f);
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
