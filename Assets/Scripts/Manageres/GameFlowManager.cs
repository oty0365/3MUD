using System.Collections;
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
        Objectile.moveSpeed = 0;
    }
    private void StartGame()
    {
        Objectile.moveSpeed = 1;
        PlatfromBase.moveSpeed = 1;
        PlatfromBase.changeMoveSpeed.Invoke();
        PlayerMove.Instance.ChangeState(PlayerBehave.Run);
        PlayerStatus.Instance.onPlayerDeath += EndGame;
        //UIManager.Instance.ImoPanel.gameObject.SetActive(false);
        ObstacleGenerator.Instance.StartSpawn();
        
    }
    public void EndGame()
    {
        UIManager.Instance.ManageActionModal(false);
        PlayerStatus.Instance.IsAlive = false;
        PlatfromBase.moveSpeed = 0;
        Objectile.moveSpeed = 0;
        PlatfromBase.changeMoveSpeed.Invoke();
        Objectile.changeMoveSpeed.Invoke();
        PlayerMove.Instance.ChangeState(PlayerBehave.Death);
        StartCoroutine(EndGameFlow());
    }
    private IEnumerator EndGameFlow()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.DeathModal();
    }
}
