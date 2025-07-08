using System;
using System.Reflection;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class AugmentUI
{
    public TextMeshProUGUI augmentUIName;
    public Image augmentUIImage;
    public TextMeshProUGUI augmentUIDesc;
}

public enum AugmentType
{
    Status,
    Effect
}
public class AugmentManager : HalfSingleMono<AugmentManager>
{

    public AugmentDatas augmentDatas;
    //private List<AugmentData> _augmentDatas = new List<AugmentData>();

    void Start()
    {
        CheckAndUploadAugments();
    }


    private void CheckAndUploadAugments()
    {
        var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<EventUploadAttribute>();
            if (attribute != null)
            {
                if (Enum.TryParse<InGameEvent>(method.Name, out var inGameEvent))
                {
                    if (method.GetParameters().Length == 0 && method.ReturnType == typeof(void))
                    {
                        var action = (Action)Delegate.CreateDelegate(typeof(Action), this, method);
                        GameEventManager.Instance.UploadEvent(inGameEvent, action);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning($"{method.Name}�� �Ű������� ���� void ��ȯ���̾�� �մϴ�.");
                    }
                }
            }
        }

        //GameEventManager.Instance.PrintEvnets();
    }


    [EventUpload]
    public void HpUp()
    {
        PlayerStatus.Instance.SetMaxHp(5);
        PlayerStatus.Instance.SetCurHp(5);
    }
    [EventUpload]
    public void DefUp()
    {
        PlayerStatus.Instance.SetDef(2);
    }
    [EventUpload]
    public void AtkUp()
    {
        PlayerStatus.Instance.SetAtk(3);
    }
    [EventUpload]
    public void SpdUp()
    {
        PlayerStatus.Instance.SetSpeed(3);
    }
    [EventUpload]
    public void WipUp()
    {
        PlayerStatus.Instance.SetWill(4);
    }
}
