using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay;
    private int _currentEggCount;
    private GameState _currentGameState;
    private void Awake() 
    {
      Instance = this;  
    }
    private void Start()
    {
      HealthManeger.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
      _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void CatController_OnCatCatched()
    {
        _playerHealthUI.AnimateDamageForAll();
        StartCoroutine(OnGameOver());       
    }

    private void HealthManager_OnPlayerDeath()
    {
      StartCoroutine(OnGameOver());
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
       
        if(_currentEggCount == _maxEggCount)
        {
            //WİN           
            _eggCounterUI.SetEggCompleted();
            ChangedGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        ChangedGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }
    public GameState GetCurrentGameState()
    {
      return _currentGameState;
    }
}