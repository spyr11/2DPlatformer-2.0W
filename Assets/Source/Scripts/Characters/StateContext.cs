using System.Collections.Generic;

public class StateContext : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public void SetStates(List<IState> states)
    {
        _states = states;

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void Switch<T>() where T : IState
    {
        IState state = _states.Find(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Update() => _currentState.Update();

    public void FixedUpdate() => _currentState.FixedUpdate();
}