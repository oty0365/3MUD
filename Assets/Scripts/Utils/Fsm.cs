using System.Collections.Generic;
using UnityEngine;
public interface IState
{
    public void OnStateEnter();
    public void OnStateUpdate();
    public void OnStateExit();
}

public class Fsm : MonoBehaviour
{

    private IState _currentState;
    private Dictionary<string, IState> states = new();
    public IState CurrentState
    {
        get => _currentState;
        set
        {
            if (value != _currentState)
            {
                _currentState.OnStateExit();
                _currentState = value;
                _currentState.OnStateEnter();
            }
        }
    }
    public void ChangeState(string key)
    {
        if (states.ContainsKey(key))
        {
            CurrentState = states[key];
        }
    }
    public void UpLoadState(string key, IState state)
    {
        if (!states.ContainsKey(key))
        {
            states.Add(key, state);
        }

    }
    private void Update()
    {
        CurrentState?.OnStateUpdate();
    }
}

