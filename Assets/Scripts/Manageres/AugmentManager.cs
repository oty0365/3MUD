using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AugmentUI
{
    public TextMeshProUGUI augmentUIName;
    public Image augmentUIImage;
    public TextMeshProUGUI augmentUIDesc;
}

public class AugmentManager : HalfSingleMono<AugmentManager>
{

    public AugmentDatas augmentDatas;
    private List<AugmentData> _augmentDatas = new List<AugmentData>();

    void Start()
    {
        CheckAndUploadAugments();
    }
    void Update()
    {
        
    }


    public void CheckAndUploadAugments()
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
                        UnityEngine.Debug.LogWarning($"{method.Name}은 매개변수가 없고 void 반환형이어야 합니다.");
                    }
                }
            }
        }

        //GameEventManager.Instance.PrintEvnets();
    }


    [EventUpload]
    public void HpUp()
    {
        PlayerStatus.Instance.SetMaxHp(3);
    }
    [EventUpload]
    public void DefUp()
    {
        PlayerStatus.Instance.SetMaxHp(3);
    }
    [EventUpload]
    public void AtkUp()
    {
        PlayerStatus.Instance.SetMaxHp(3);
    }
    [EventUpload]
    public void SpdUp()
    {
        //PlayerInfo.Instance.playerBasicAttackDamage += 15;
    }
    [EventUpload]
    public void WipUp()
    {

    }
}
