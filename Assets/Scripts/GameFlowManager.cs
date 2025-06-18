using UnityEngine;

public class GameFlowManager : HalfSingleMono<GameFlowManager>
{
    [SerializeField] private GameObject tabPnel;
    private bool _hasStarted;
    public bool HasStarted
    {
        get => _hasStarted;
        set
        {
            if (value != _hasStarted)
            {
                if (value)
                {
                    StartGame();
                }
                _hasStarted = value;
            }
           
        }
    }
    void Start()
    {
        InitGame();
        HasStarted = false;
    }
    void Update()
    {
        
    }
    private void InitGame()
    {
        PlatfromBase.moveSpeed = 0;
    }
    private void StartGame()
    {
        PlatfromBase.moveSpeed = 1;
        PlatfromBase.changeMoveSpeed.Invoke();
        PlayerMove.Instance.ChangeState(PlayerBehave.Run);
        tabPnel.SetActive(false);
        
    }
}
