using UnityEngine;

public class PlayerİnteractiveController : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) 
   {
    if(other.gameObject.TryGetComponent<ICollectibles>(out var collectible))
    {
      collectible.Collect();
    }
   }
}
