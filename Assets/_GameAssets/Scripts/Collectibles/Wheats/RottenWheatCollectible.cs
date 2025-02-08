using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private float _movementDecreaseSpeed;
  [SerializeField] private float _resetBoostDuration;

  public void Collect()
  {
    _playerController.SetMovementSpeed(_movementDecreaseSpeed, _resetBoostDuration);
    Destroy(gameObject);
  }
}
