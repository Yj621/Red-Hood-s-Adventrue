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
        if (PlayerController.Instance.isAttack)
            return;

        if (other.CompareTag("Enemy"))
        {
            HandleAttack(other.GetComponent<EnemyController>());
        }
        else if (other.CompareTag("Boss"))
        {
            HandleAttack(other.GetComponent<BossController>());
        }
    }

    private void HandleAttack(IEnemy target)
    {
        if (target == null)
            return;

        if (PlayerController.Instance.isCut)
        {
            PlayerController.Instance.CutAttack(target);
        }
        else if (PlayerController.Instance.isSeriesCut)
        {
            PlayerController.Instance.SeriesCutAttack(target);
        }
    }   
}
