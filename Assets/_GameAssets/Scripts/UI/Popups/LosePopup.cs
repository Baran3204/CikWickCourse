using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MaskTransitions;

public class LosePopup : MonoBehaviour
{
     [Header("References")]

    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;
   private void OnEnable() 
   {
        
        _timerText.text = _timerUI.GetFinalTime();

        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);   
        _mainMenuButton.onClick.AddListener(() => 
        {
           TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
        });
   }
    private void OnTryAgainButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}
