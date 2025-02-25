using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private PlayableDirector _playableDirector;

    private void Awake() 
    {
        _playableDirector = GetComponent<PlayableDirector>();    
    }

    private void OnEnable() 
    {
      _playableDirector.Play();
      _playableDirector.stopped += OnTimelineFinished;  
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
       _gameManager.ChangedGameState(GameState.Play);
    }
}
