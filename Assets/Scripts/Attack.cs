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
        if (other.gameObject.tag == "Enemy" && !PlayerController.Instance.isAttack)
        {
            Debug.Log("Trigger");
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {

                PlayerController.Instance.Attack(enemy);
            }
        }
    }
}
