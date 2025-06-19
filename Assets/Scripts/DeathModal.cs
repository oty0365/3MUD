using TMPro;
using UnityEngine;

public class DeathModal : MonoBehaviour
{
    private Animator _ani;
    [SerializeField] private TextMeshProUGUI deathTmp;
    [SerializeField] private TextMeshProUGUI msgTmp;
    [SerializeField] private TextMeshProUGUI goldTmp;
    [SerializeField] private TextMeshProUGUI goldTextTmp;
    [SerializeField] private TextMeshProUGUI runTmp;
    [SerializeField] private TextMeshProUGUI runTextTmp;
    [SerializeField] private TextMeshProUGUI restartTmp;
    private void Awake()
    {
        _ani = gameObject.GetComponent<Animator>();
    }
    public void StartModal()
    {
        _ani.Play("DeathModalActive");
        UpdateDeathModal();
    }
    public void EndModal()
    {
        _ani.Play("DeathModalReturn");
        ManageDeathModal(false);
        UIManager.Instance.ManageActionModal(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void ManageDeathModal(bool isActive)
    {
        deathTmp.gameObject.SetActive(isActive);
        msgTmp.gameObject.SetActive(isActive);
        goldTmp.gameObject.SetActive(isActive);
        goldTextTmp.gameObject.SetActive(isActive);
        runTmp.gameObject.SetActive(isActive);
        runTextTmp.gameObject.SetActive(isActive);
        restartTmp.gameObject.SetActive(isActive);
    }
    public void UpdateDeathModal()
    {
        var scripts = Scripter.Instance;
        deathTmp.text = scripts.scripts["YouDied"].currentText;
        var index = Random.Range(1, 16);
        msgTmp.text = scripts.scripts["DieMsg-" + index].currentText;
        goldTextTmp.text = scripts.scripts["ObtainedGold"].currentText;
        goldTmp.text = PlayerInfo.Instance.CurrentRunCoinCount.ToString();
        runTextTmp.text = scripts.scripts["RunAmount"].currentText;
        restartTmp.text = scripts.scripts["PressToRespawn"].currentText;
    }
}
