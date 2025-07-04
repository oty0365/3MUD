using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventUpLoader
{
    public void UpLoad();
}

public class GameFlowManager : HalfSingleMono<GameFlowManager>
{
    public event Action startIdel; 
   // public event 
    public List<MonoBehaviour> eventUpLoaders;
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
                else
                {
                    StopGame();
                }
                _hasStarted = value;
            }
            
        }
    }

    protected override void Awake()
    {
        base.Awake();
        PlayerStatus.Instance.onPlayerDeath -= EndGame;
        PlayerStatus.Instance.onPlayerDeath += EndGame;
    }

    void Start()
    {
        InitGame();
        HasStarted = false;
    }

    private void InitGame()
    {
        PlatformBase.changeMoveSpeed?.Invoke(0);
        foreach (var obj in eventUpLoaders)
        {
            var loader = obj.GetComponent<IEventUpLoader>();
            if (loader != null)
            {
                loader.UpLoad();
            }
            else
            {
                Debug.LogWarning($"{obj.name}에 IEventUpLoader가 없습니다.");
            }

        }
    }

    private void StartGame()
    {
        Objectile.moveSpeed = 1;
        Objectile.changeMoveSpeed?.Invoke(1);
        PlatformBase.moveSpeed = 1;
        PlatformBase.changeMoveSpeed?.Invoke(1);
        PlayerBehavior.Instance.ChangeState(PlayerBehave.Run);
        ObstacleGenerator.Instance.StartSpawn();
    }

    public void EndGame()
    {
        UIManager.Instance.ManageActionModal(false);
        PlayerStatus.Instance.IsAlive = false;
        StopGame();
        PlayerBehavior.Instance.ChangeState(PlayerBehave.Death);
        StartCoroutine(EndGameFlow());
    }
    public void StopGame()
    {
        startIdel?.Invoke();
        Objectile.moveSpeed = 0;
        PlatformBase.moveSpeed = 0; 
        Objectile.changeMoveSpeed?.Invoke(0);
        PlatformBase.changeMoveSpeed?.Invoke(0);
        ObstacleGenerator.Instance.StopSpawn();
    }

    private IEnumerator EndGameFlow()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.DeathModal();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HasStarted = false;
            UIManager.Instance.AugmentSelection();
        }
    }
}
