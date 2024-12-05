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
        if (other.gameObject.tag != "Enemy" || PlayerController.Instance.isAttack)
            return;

        var enemy = other.GetComponent<EnemyController>();
        var boss = other.GetComponent<BossController>();

        if (PlayerController.Instance.isCut)
        {
            if (enemy != null)
                PlayerController.Instance.CutAttack(enemy);
            else if (boss != null)
                PlayerController.Instance.CutAttack(boss);
        }

        else if (PlayerController.Instance.isSeriesCut)
        {
            if (enemy != null)
                PlayerController.Instance.SeriesCutAttack(enemy);
            else if (boss != null)
                PlayerController.Instance.SeriesCutAttack(boss);
        }
    }

}
