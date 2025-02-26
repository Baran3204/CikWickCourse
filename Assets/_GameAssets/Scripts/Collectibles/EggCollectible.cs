using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectibles
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
    }
}
