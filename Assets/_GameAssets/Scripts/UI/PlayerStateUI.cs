using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
     
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;

    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;

    [Header("Images")]

    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;

    [Header("Sprites")]

    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;
    
    [Header("Settings")]

    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;

    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => _holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => _rottenBoosterWheatImage;
    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    private void Awake() 
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }
    private void Start() 
    {
       _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;

       SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
       switch(playerState)
       {
           case PlayerState.Idle:
           case PlayerState.Move:
                    SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                    break;
           case PlayerState.Slide:
           case PlayerState.SlideIdle:
                    SetStateUserInterface(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                    break;
       }
    }

    private void SetStateUserInterface(Sprite playerWalkingSprite, Sprite playerSlidingSprite, RectTransform playerActiveTransform, RectTransform playerPassiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        playerActiveTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        playerPassiveTransform.DOAnchorPosX(-90F, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterFaces(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);
        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }

    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterFaces(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }
}
