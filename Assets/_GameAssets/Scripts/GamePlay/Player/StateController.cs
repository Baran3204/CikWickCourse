using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentplayerState = PlayerState.Idle;


    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }
    public void ChangeState(PlayerState newPlayerState)
    {
        if (_currentplayerState == newPlayerState) { return; }

        _currentplayerState = newPlayerState;
    }

    public PlayerState GetCurrentState()
    {
        return _currentplayerState;
    }
}
