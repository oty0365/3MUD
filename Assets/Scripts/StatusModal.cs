using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusModal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackTextTmp;
    public TextMeshProUGUI attackTmp;
    [SerializeField] private TextMeshProUGUI deffendTextTmp;
    public TextMeshProUGUI deffendTmp;
    [SerializeField] private TextMeshProUGUI speedTextTmp;
    public TextMeshProUGUI speedTmp;
    [SerializeField] private TextMeshProUGUI willTextTmp;
    public TextMeshProUGUI willTmp;
    [SerializeField] private Image coinImg;
    public TextMeshProUGUI coinTmp;
    [SerializeField] private Image manaStoneImg;
    public TextMeshProUGUI manaStoneTmp;

    public void InitStatusModal()
    {
        var script = Scripter.Instance;
        attackTextTmp.text = script.scripts["AttackTmp"].currentText;
        deffendTextTmp.text = script.scripts["DeffendTmp"].currentText;
        speedTextTmp.text = script.scripts["SpeedTmp"].currentText;
        willTextTmp.text = script.scripts["WillTmp"].currentText;

    }

    public void SetAttack(float value) => attackTmp.text = value.ToString();
    public void SetDefense(float value) => deffendTmp.text = value.ToString();
    public void SetSpeed(float value) => speedTmp.text = value.ToString();
    public void SetWill(float value) => willTmp.text = value.ToString();
    public void SetCoin(int value) => coinTmp.text = value.ToString();
}
