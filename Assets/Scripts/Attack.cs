using UnityEngine;

public class Attack : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("Attack");
        if(other.gameObject.tag == "Enemy")
        {
            PlayerController.Instance.DealDamage(1);
            Debug.Log("Attack");
        }
    }
}
