using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : HalfSingleMono<UIManager>,IEventUpLoader
{
    //public
    //Serializefield-private
    [SerializeField] private Image darkModal;
    [SerializeField] private Slider hpBarUI;
    [SerializeField] private DeathModal deathModal;
    [SerializeField] private StatusModal statusModal;
    [SerializeField] private Image jumpBtn;
    [SerializeField] private Image slideBtn;
    [SerializeField] private Image[] itemBtn;
    [SerializeField] private Image skillBtn;
    //private

    
    private void Start()
    {

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
    }

    private void InitModal()
    {
        DarkFade();
        darkModal.gameObject.SetActive(true);
        deathModal.gameObject.SetActive(false);
        statusModal.InitStatusModal();
        ManageActionModal(true);

    }

    public void DarkFade()
    {
        Debug.Log(darkModal.material.GetFloat("_Progress"));
        darkModal.material.SetFloat("_Progress", 1);
    }

    public void DarkFadeIn(float duration = 1f)
    {
        StartCoroutine(FadeRoutine(0f, 1f, duration));
    }

    public void DarkFadeOut(float duration = 1f)
    {
        StartCoroutine(FadeRoutine(1f, 0f, duration));
    }

    private IEnumerator FadeRoutine(float from, float to, float duration)
    {
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
        deathModal.gameObject.SetActive(true);
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
    
}
