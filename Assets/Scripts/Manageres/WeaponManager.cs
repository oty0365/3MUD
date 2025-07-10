using System;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponCode
{
    
}

public class WeaponManager : HalfSingleMono<WeaponManager>,IEvent
{
    [SerializeField] private AugmentDatas weaponAugmentData;
    [SerializeField] private WeaponDatas weaponDatas;
    private Dictionary<string,WeaponData> weaponDict = new ();
    private string _currentSelectedWeapon;

    public void Initial()
    {
        foreach (var i in weaponDatas.weaponData)
        {
            //weaponDict.Add(i);
        }
    }

    public void ChangeSelectedWeapon(string weaponCode)
    {
        _currentSelectedWeapon = weaponCode;
    }
    
    public void ChangeOrEquipWeapon()
    {
        //현제 선택된 무기를 플레이어 웨폰 인벤토리에 넣는다
        //weaponDict[_currentSelectedWeapon]
    }
    
    public void UploadEvent(InGameEvent inGameEvent, Action action)
    {
        GameEventManager.Instance.UploadEvent(inGameEvent, action);
    }

    public void RemoveEvent(InGameEvent inGameEvent)
    {
        GameEventManager.Instance.RemoveEvent(inGameEvent);
    }
}
