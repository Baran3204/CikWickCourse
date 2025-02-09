using System;
using UnityEngine.UI;
using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectibles
{
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  [SerializeField] private PlayerStateUI _playerStateUI;

  private RectTransform _playerBoosterTransform;
  private Image _playerBoosterImage;

  private void Awake() 
  {
    _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
    _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
  }
  public void Collect()
  {
    _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);

    _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetGoldBoosterWheatImage, _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite, _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);
    
    Destroy(gameObject);
  }
}
