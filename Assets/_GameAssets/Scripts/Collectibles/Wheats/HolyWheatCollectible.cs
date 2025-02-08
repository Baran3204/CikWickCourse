using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
   
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private float _jumpingIncreaseForce;
  [SerializeField] private float _resetBoostDuration;

  public void Collect()
  {
    _playerController.SetJumpingForce(_jumpingIncreaseForce, _resetBoostDuration);
    Destroy(gameObject);
  }
}
