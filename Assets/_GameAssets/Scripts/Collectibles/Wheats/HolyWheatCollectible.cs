using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectibles
{
   
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private WheatDesignSO _wheatDesignSO;

  public void Collect()
  {
    _playerController.SetJumpingForce(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
  }
}
