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
        Obstacle.moveSpeed = 0;
    }
    private void StartGame()
    {
        Obstacle.moveSpeed = 1;
        PlatfromBase.moveSpeed = 1;
        PlatfromBase.changeMoveSpeed.Invoke();
        PlayerMove.Instance.ChangeState(PlayerBehave.Run);
        UIManager.Instance.ImoPanel.gameObject.SetActive(false);
        ObstacleGenerator.Instance.StartSpawn();
        
    }
    public void StopGame()
    {
        PlayerInfo.Instance.isAlive = false;

        PlatfromBase.moveSpeed = 0;
        Obstacle.moveSpeed = 0;
        PlatfromBase.changeMoveSpeed.Invoke();
        Obstacle.changeMoveSpeed.Invoke();
        PlayerMove.Instance.ChangeState(PlayerBehave.Death);
    }
}
