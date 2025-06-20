using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : HalfSingleMono<UIManager>
{
    public TextMeshPro ImoPanel;
    public Slider hpBarUI;
    public DeathModal deathModal;
    public StatusModal statusModal;
    public Image jumpBtn;
    public Image slideBtn;
    public Image[] itemBtn;
    public Image skillBtn;


    public void Start()
    {
        InitModal();
    }
    public void InitModal()
    {
        deathModal.gameObject.SetActive(false);
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
}
