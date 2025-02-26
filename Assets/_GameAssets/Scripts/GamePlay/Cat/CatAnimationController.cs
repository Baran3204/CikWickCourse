using Unity.VisualScripting;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
   [SerializeField] private Animator _catAnimator;
   private CatStateController _catStateController; 

   private void Awake() 
   {
        _catStateController = GetComponent<CatStateController>();
   }

   private void Update() 
   {
    if(GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
            {
               _catAnimator.enabled = false;
                return;
            }
     SetCatAnimations();
   }

   private void SetCatAnimations()
   {
    _catAnimator.enabled = true;
     var currentCatState = _catStateController.GetCurrentState();

     switch(currentCatState)
     {
        case CatState.Idle:
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_IDLING, true);
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_WALKING, false);
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_RUNNING, false);
            break;
        case CatState.Walking:
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_IDLING, false);
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_WALKING, true);
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_RUNNING, false);
            break;
        case CatState.Running:
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_RUNNING, true);
            break;
        case CatState.Attacking:
            _catAnimator.SetBool(Consts.SetCatAnimations.IS_ATTACKING, true);
            break;
     }
   }
}
