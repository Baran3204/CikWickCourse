using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    private int _currentEggCount;
    private GameState _currentGameState;
    private void Awake() 
    {
      Instance = this;  
    }
    private void OnEnable() 
    {
      ChangedGameState(GameState.Play);  
    }

    public void ChangedGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
    }
    public void OnEggCollected()
    {
        _currentEggCount++;    
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);   
        Debug.Log("Egg Count: "+ _currentEggCount);

        if(_currentEggCount == _maxEggCount)
        {
            //WİN
            Debug.Log("Game Win!");
            _eggCounterUI.SetEggCompleted();
            ChangedGameState(GameState.GameOver);
        }
    }

    public GameState GetCurrentGameState()
    {
      return _currentGameState;
    }
}