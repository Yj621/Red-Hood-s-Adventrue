using UnityEngine;

public class DeadZone : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "Player")
    {
        Debug.Log("Die2");
        PlayerController.Instance.Die();
    }
   }
}
