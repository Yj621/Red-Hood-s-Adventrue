using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = GameManager.Instance.playerController;
    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.isAttack)
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

        if (player.isCut)
        {
            player.CutAttack(target);
        }
        else if (player.isSeriesCut)
        {
            player.SeriesCutAttack(target);
        }
    }   
}
