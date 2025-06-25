using UnityEngine;

public class PlayerSkin : HalfSingleMono<PlayerSkin>,IEventUpLoader
{
    [SerializeField]
    private CharacterData _currentSkn;
    public CharacterData CurrentSkin
    {
        get => _currentSkn;
        set
        {
            if (value != _currentSkn)
            {
                _currentSkn = value;
                //OnChangedCharacter();
            }
        }
    }
    public void InitSkin()
    {
        var playerStatus = PlayerStatus.Instance;
        playerStatus.CurrentRunAttack = CurrentSkin.Atk;
        playerStatus.CurrentRunDeffence = CurrentSkin.Def;
        playerStatus.CurrentRunSpeed = CurrentSkin.Spd;
        playerStatus.CurrentRunWill = CurrentSkin.Wip;
    }
    protected override void Awake()
    {
        base.Awake();

    }
    public void UpLoad()
    {
        InitSkin();
    }

}
