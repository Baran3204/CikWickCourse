using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectibles
{
    
  [SerializeField] private PlayerController _playerController;
 [SerializeField] private WheatDesignSO _wheatDesignSO;

  public void Collect()
  {
    _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
  }
}
