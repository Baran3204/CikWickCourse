using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectibles
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
