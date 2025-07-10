using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public float basicAttackDamage;
    public string weaponDescription;
}
