using System;
using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectibles
{
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  public void Collect()
  {
    _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
  }
}
