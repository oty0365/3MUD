using UnityEngine;

public class GameFlowManager : HalfSingleMono<GameFlowManager>
{
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
            }
        }
    }
    void Start()
    {
        InitGame();
        _hasStarted = false;
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
        
    }
}
