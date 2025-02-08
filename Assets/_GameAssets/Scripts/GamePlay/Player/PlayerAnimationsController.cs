using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
   [SerializeField] private Animator _playerAnimator;

   private PlayerController _playerController;
   private StateController _stateController;

   private void Awake() 
   {
     _playerController = GetComponent<PlayerController>();
     _stateController = GetComponent<StateController>();
   }
   private void Start() 
   {
        _playerController.OnPlayerjumped += PlayerController_OnPlayerjumped;   
        Invoke(nameof(ResetJumping), 0.5f);
   }
    private void Update() 
   {
      SetPlayerAnimations();
   }

   private void PlayerController_OnPlayerjumped()
    {
       _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
    }
    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

   private void SetPlayerAnimations()
   {
    var currentState = _stateController.GetCurrentState();

    switch(currentState)
        {
            case PlayerState.Idle:
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;
            case PlayerState.Move:
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;
            case PlayerState.SlideIdle:
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;
             case PlayerState.Slide:
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
            _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
   }
}
