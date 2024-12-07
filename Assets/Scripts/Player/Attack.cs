using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum AttackType
    {
        CutAttack,
        SeriesCutAttack
    }

    public AttackType attackType; // Inspector에서 설정할 변수

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

        switch (attackType)
        {
            case AttackType.CutAttack:
                if (player.isCut)
                {
                    player.CutAttack(target);
                }
                break;

            case AttackType.SeriesCutAttack:
                if (player.isSeriesCut)
                {
                    player.SeriesCutAttack(target);
                }
                break;
        }
    }
}
