using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public enum PlayerEffects
{
    Hit,
    CoinConsume,
    Heal
}
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerEffecter : HalfSingleMono<PlayerEffecter>
{
    //SerialieField private
    [SerializeField] private TextMeshPro amountPanel;
    //private
    private SpriteRenderer _sr;
    private Coroutine _currentHitFlow;
    private Coroutine _currentInfiateBlinkFlow;
    //private Dictionary<PlayerEffects,Action> e
    //func-public
    public void PlayPlayerEffect(PlayerEffects playerEffets,float amount)
    {
        switch (playerEffets)
        {
            case PlayerEffects.Hit:
                OnHitEffect(amount);
                break;
            case PlayerEffects.CoinConsume:
                OnCoinEffect((int)amount);
                break;
            case PlayerEffects.Heal:
                OnHealEffect(amount);
                break;
        }
    }
    public void InfinateBlink(float time)
    {
        if (_currentInfiateBlinkFlow != null)
        {
            StopCoroutine(_currentInfiateBlinkFlow);
        }
        _currentInfiateBlinkFlow = StartCoroutine(InfinateBlinkFlow(time));
    }
    //func-private
    private void OnCoinEffect(int coin)
    {
        if (coin > 0)
        {
            amountPanel.text = "+" + coin;
        }
        else
        {
            amountPanel.text = "-" + coin;
        }
        if (_currentHitFlow != null)
        {
            StopCoroutine(_currentHitFlow);
        }
        _currentHitFlow = StartCoroutine(AmountTextFlow(Color.yellow));
    }
    private void OnHealEffect(float heal)
    {
        amountPanel.text = "+" + heal;
        if (_currentHitFlow != null)
        {
            StopCoroutine(_currentHitFlow);
        }
        _currentHitFlow = StartCoroutine(AmountTextFlow(new Color(32,59,28)));
    }
    private void OnHitEffect(float damage)
    {
        amountPanel.text = "-" + damage;
        if (_currentHitFlow != null)
        {
            StopCoroutine(_currentHitFlow);
        }
        _currentHitFlow = StartCoroutine(AmountTextFlow(Color.red));
    }
    private IEnumerator AmountTextFlow(Color targetColor)
    {
        var panel = amountPanel;
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
    private IEnumerator InfinateBlinkFlow(float time)
    {
        var section = time / 8;
        var counter = 1;
        for (var i = 0f; i <= time; i += Time.deltaTime)
        {
            if (i > counter * section)
            {
                counter++;
                _sr.enabled = !_sr.enabled;
            }
            yield return null;
        }
        _sr.enabled = true;
        yield return new WaitForSeconds(time);
    }
    //lifefunc-private
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        PlayerStatus.Instance.playerInteractEffects += PlayPlayerEffect;
        PlayerStatus.Instance.blinkEffect += InfinateBlink;
    }
}
