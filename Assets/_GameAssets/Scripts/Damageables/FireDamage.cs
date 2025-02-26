using UnityEngine;

public class FireDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private float _force = 10;
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {

        HealthManeger.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);
        AudioManager.Instance.Play(SoundType.ChickSound);
        Destroy(gameObject);
    }
}
