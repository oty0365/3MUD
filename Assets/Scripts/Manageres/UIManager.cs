using UnityEngine;
using UnityEngine.UI;

public class UIManager : HalfSingleMono<UIManager>
{
    //public
    //Serializefield-private
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
        PlayerStatus.Instance.onMaxHpChangeUI += OnMaxHpChange;
        PlayerStatus.Instance.onHpChangeUI += OnHpChange;
        PlayerStatus.Instance.onCoinChangeUI += OnCoinConsume;
        InitUI();
        InitModal();
    }
    private void InitModal()
    {
        deathModal.gameObject.SetActive(false);
        statusModal.InitStatusModal();
        ManageActionModal(true);
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
    public void OnCoinConsume(int count)
    {
        statusModal.coinTmp.text = count.ToString();
    }
    public void InitUI()
    {
        OnMaxHpChange(PlayerStatus.Instance.PlayerMaxHp);
        OnHpChange(PlayerStatus.Instance.PlayerCurHp);
    }
    
}
