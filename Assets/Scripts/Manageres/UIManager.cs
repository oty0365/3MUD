using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface ShowHider
{
    public void Show();
    public void Hide();
}

public class UIManager : HalfSingleMono<UIManager>,IEventUpLoader
{
    //public
    //Serializefield-private
    [SerializeField] private Image darkModal;
    [SerializeField] private TextMeshProUGUI dateTmp;
    [SerializeField] private Slider hpBarUI;
    [SerializeField] private DeathModal deathModal;
    [SerializeField] private StatusModal statusModal;
    [SerializeField] private AugmentSelectionModal augmentSelectionModal;
    [SerializeField] private Image jumpBtn;
    [SerializeField] private Image slideBtn;
    [SerializeField] private Image[] itemBtn;
    [SerializeField] private Image skillBtn;
    //private
    private Coroutine _currentFadeFlow;
    public Action<bool> setGame;
    
    private void Start()
    {
        augmentSelectionModal.Hide();
        //initializePlayer?.Invoke();
    }

    public void UpLoad()
    {
        
        InitUI();
        InitModal();
        var playerStatus = PlayerStatus.Instance;
        playerStatus.onMaxHpChangeUI += OnMaxHpChange;
        playerStatus.onHpChangeUI += OnHpChange;
        playerStatus.onCoinChangeUI += OnCoinConsume;
        playerStatus.onAtkChangeUI += OnAtkChange;
        playerStatus.onSpdChangeUI += OnSpdChange;
        playerStatus.onDefChangeUI += OnDefChange;
        playerStatus.onWipChangeUI += OnWipChange;
        setGame+=GameFlowManager.Instance.SetGame;
        augmentSelectionModal.fadeOut += DarkFadeOut;
    }

    private void InitModal()
    {
        SetDarkFade(1);
        SetDay(1, 1.5f);
        deathModal.Hide();
        statusModal.InitStatusModal();
        ManageActionModal(true);

    }

    public void SetDay(int date,float time)
    {
        dateTmp.text = "Day " + date;
        StartCoroutine(SetDayTextFlow(date, time));
    }
    private IEnumerator SetDayTextFlow(int date,float time)
    {
        dateTmp.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        dateTmp.gameObject.SetActive(false);
        DarkFadeOut();
    }

    public void SetDarkFade(float amount)
    {
        darkModal.material.SetFloat("_Progress", amount);
    }

    public void DarkFadeIn()
    {
        if (_currentFadeFlow != null)
        {
            StopCoroutine(_currentFadeFlow);
        }
        StartCoroutine(FadeRoutine(0f, 1f, 1f));
    }

    public void DarkFadeOut()
    {
        if (_currentFadeFlow != null)
        {
            StopCoroutine(_currentFadeFlow);
        }
        StartCoroutine(FadeRoutine(1f, 0f,1f));
    }

    private IEnumerator FadeRoutine(float from, float to, float duration)
    {
        darkModal.gameObject.SetActive(true);
        float elapsed = 0f;
        Material mat = darkModal.material;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float progress = Mathf.Lerp(from, to, t);
            mat.SetFloat("_Progress", progress);
            yield return null;
        }
        mat.SetFloat("_Progress", to);
        if (to <= 0)
        {
            darkModal.gameObject.SetActive(false);
        }
    }

    public void ManageActionModal(bool mode)
    {
        jumpBtn.gameObject.SetActive(mode);
        slideBtn.gameObject.SetActive(mode);
        for (var i = 0; i < itemBtn.Length; i++)
        {
            itemBtn[i].gameObject.SetActive(mode);
        }
        skillBtn.gameObject.SetActive(mode);
    }
    public void DeathModal()
    {
        deathModal.Show();
        deathModal.StartModal();
    }
    public void OnMaxHpChange(float amount)
    {
        hpBarUI.maxValue = amount;
    }
    public void OnHpChange(float amount)
    {
        hpBarUI.value = amount;
    }
    public void OnAtkChange(float amount)
    {
        statusModal.SetAttack(amount);
    }
    public void OnDefChange(float amount)
    {
        statusModal.SetDefense(amount);
    }
    public void OnSpdChange(float amount)
    {
        statusModal.SetSpeed(amount);
    }
    public void OnWipChange(float amount)
    {
        statusModal.SetWill(amount);
    }
    public void OnCoinConsume(int count)
    {
        statusModal.SetCoin(count);
    }
    public void InitUI()
    {
        OnMaxHpChange(PlayerStatus.Instance.PlayerMaxHp);
        OnHpChange(PlayerStatus.Instance.PlayerCurHp);
    }
    private void ReplaceAugmentModal()
    {
        augmentSelectionModal.Show();
        augmentSelectionModal.RandomizeCard();
    }
    private IEnumerator AugmentSelectionFlow()
    {
        if (_currentFadeFlow != null)
        {
            StopCoroutine(_currentFadeFlow);
        }
        _currentFadeFlow = StartCoroutine(FadeRoutine(0f, 1f, 1));
        yield return _currentFadeFlow;
        yield return new WaitForSeconds(0.5f);
        ReplaceAugmentModal();

    }
    public void AugmentSelection()
    {
        setGame?.Invoke(false);
        StartCoroutine(AugmentSelectionFlow());
    }
    
}
