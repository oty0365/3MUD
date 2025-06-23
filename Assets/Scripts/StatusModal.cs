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
}
