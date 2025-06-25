using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string _characterCode;
    public string CharacterCode
    {
        get => _characterCode;
    }

    [SerializeField]
    private string _characterName;
    public string CharacteName
    {
        get => _characterName;
    }

    [SerializeField]
    private string _characterDesc;
    public string CharacterDesc
    {
        get => _characterDesc;
    }

    [SerializeField]
    private Sprite _icon;
    public Sprite Icon
    {
        get => _icon;
    }

    [SerializeField]
    private float _hp;
    public float Hp
    {
        get => _hp;
    }

    [SerializeField]
    private float _atk;
    public float Atk
    {
        get => _atk;
    }

    [SerializeField]
    private float _def;
    public float Def
    {
        get => _def;
    }

    [SerializeField]
    private float _spd;
    public float Spd
    {
        get => _spd;
    }

    [SerializeField]
    private float _wip;
    public float Wip
    {
        get => _wip;
    }

    [SerializeField]
    private AnimatorOverrideController _overrideController;
    public AnimatorOverrideController OverrideController
    {
        get => _overrideController;
    }
}
