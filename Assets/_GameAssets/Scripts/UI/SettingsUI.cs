using System;
using DG.Tweening;
using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private GameObject _settingsPopupObject;
   [SerializeField] private GameObject _blackBackgroundObject;

   [Header("Buttons")]
   [SerializeField] private Button _settingsButton;
   [SerializeField] private Button _musicButton;
   [SerializeField] private Button _soundButton;
   [SerializeField] private Button _resumeButton;
   [SerializeField] private Button _mainMenuButton;
   
   [Header("Sprites")]

   [SerializeField] private Sprite _musicActiveSprite;
   [SerializeField] private Sprite _musicPassiveSprite;
   [SerializeField] private Sprite _soundActiveSprite;
   [SerializeField] private Sprite _soundPassiveSprite;

   [Header("Settings")]
   [SerializeField] private float _animationDuration;
   private Image _blackBackgroundImage;
   private bool _isSoundActive = true;
   private bool _isMusicActive = true;
   private void Awake() 
   {
      _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
      _settingsPopupObject.transform.localScale = Vector3.zero;

      _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
      _resumeButton.onClick.AddListener(OnResumeButtonClicked);
      _mainMenuButton.onClick.AddListener(() => 
      {
        AudioManager.Instance.Play(SoundType.TransitionSound);

         TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
      });
      _musicButton.onClick.AddListener(OnMusicButtonClicked);
      _soundButton.onClick.AddListener(OnSoundButtonClicked);
   }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isSoundActive = !_isSoundActive;
        _soundButton.image.sprite = _isSoundActive ? _soundActiveSprite : _soundPassiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!_isSoundActive);
    }

    private void OnMusicButtonClicked()
    {
       AudioManager.Instance.Play(SoundType.ButtonClickSound);
      _isMusicActive = !_isMusicActive;
      _musicButton.image.sprite = _isMusicActive ? _musicActiveSprite : _musicPassiveSprite;
      BackgroundMusic.Instance.SetMusicMute(!_isMusicActive);
    }

    private void OnSettingsButtonClicked()
   {
      GameManager.Instance.ChangedGameState(GameState.Pause);
      AudioManager.Instance.Play(SoundType.ButtonClickSound);


      _blackBackgroundObject.SetActive(true);
      _settingsPopupObject.SetActive(true);

      _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
      _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
   }

   private void OnResumeButtonClicked()
   {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);

      _blackBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);
      _settingsPopupObject.transform.DOScale(1f, _animationDuration).SetEase(Ease.InBack).OnComplete(() => 
      {
          GameManager.Instance.ChangedGameState(GameState.Resume);
          _blackBackgroundObject.SetActive(false);
          _settingsPopupObject.SetActive(false);
      });      
   }
}
