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

    private bool _isCatCatched;
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
      if(!_isCatCatched)
      {
        _playerHealthUI.AnimateDamageForAll();
        StartCoroutine(OnGameOver(true));    
        CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f);  
        _isCatCatched = true;
      }       
    }

    private void HealthManager_OnPlayerDeath()
    {
      StartCoroutine(OnGameOver(false));
    }

    private void OnEnable() 
    {
      ChangedGameState(GameState.CutScene);
      BackgroundMusic.Instance.PlayBackgroundMusic(true); 
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

    private IEnumerator OnGameOver(bool isCatCatched)
    {
        yield return new WaitForSeconds(_delay);
        ChangedGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();   
        if(isCatCatched) { AudioManager.Instance.Play(SoundType.SpatulaSound); }
    }
    public GameState GetCurrentGameState()
    {
      return _currentGameState;
    }
}