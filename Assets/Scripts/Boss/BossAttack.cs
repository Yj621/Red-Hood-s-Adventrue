using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Collider2D attackRange;

    void Start()
    {
        attackRange = GetComponent<Collider2D>();   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerController.DealDamage(20);
        }
    }

    void Update()
    {
        
    }
}
