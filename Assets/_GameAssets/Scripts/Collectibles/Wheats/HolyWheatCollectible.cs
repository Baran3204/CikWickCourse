using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectibles
{
   
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  [SerializeField] private PlayerStateUI _playerStateUI;

  private RectTransform _playerBoosterTransform;
  private Image _playerBoosterImage;

  private void Awake() 
  {
    _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
    _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
  }
  public void Collect()
  {
    _playerController.SetJumpingForce(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);

     _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetHolyBoosterWheatImage, _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite, _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);

    Destroy(gameObject);
  }
}
