using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectibles
{
    
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  [SerializeField] private PlayerStateUI _playerStateUI;

  private RectTransform _playerBoosterTransform;
  private Image _playerBoosterImage;

  private void Awake() 
  {
    _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
    _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
  }
  public void Collect()
  {
    _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);

    _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetRottenBoosterWheatImage, _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite, _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);
    CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
     AudioManager.Instance.Play(SoundType.PickupBadSound);
    Destroy(gameObject);
  }
}
