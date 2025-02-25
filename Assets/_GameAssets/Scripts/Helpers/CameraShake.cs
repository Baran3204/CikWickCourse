using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance {get; private set;} 
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private int _startingİntensity;
    private CinemachineBasicMultiChannelPerlin _cineMachine;
    private void Awake() 
    {
        Instance = this;
        _cineMachine = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }   
    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        _cineMachine.AmplitudeGain = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;
    }

    public void ShakeCamera(float intensity, float time, float delay = 0f)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));
    }

    private void Update() 
    {
      if(_shakeTimer > 0f)
      {
        _shakeTimer -= Time.deltaTime;
        if(_shakeTimer <= 0f)
        {
                _cineMachine.AmplitudeGain
                 = Mathf.Lerp(_startingİntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
        }
      }  
    }
}
