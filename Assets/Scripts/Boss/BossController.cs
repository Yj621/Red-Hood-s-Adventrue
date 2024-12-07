using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IEnemy
{
    Player player;
    PlayerRange playerRange;
    AttackRange attackRange;

    Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private GameObject damageText;
    [SerializeField] private Image hpGauge;


    [SerializeField] private float speed = 1f;
    [SerializeField] private float hp = 200;
    [SerializeField] private float maxHp = 200;
    [SerializeField] private float attackCooldownDuration = 1.5f; // 공격 쿨타임 시간 (초)
    private float moveSpeed = 1f;
    private float nextHurtHp;

    [SerializeField] private int damage;

    Vector2 vx;
    public Transform damagePos;

    private bool isHurt = false;
    [SerializeField] private bool isDie = false;
    [SerializeField] private bool isAttackCoolDown = false;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        nextHurtHp = hp - 10;

        attackRange = transform.GetChild(2).gameObject.GetComponent<AttackRange>();
        playerRange = transform.GetChild(3).gameObject.GetComponent<PlayerRange>();
    }

    void Update()
    {
        if (isDie) return; // 죽었으면 어떤 행동도 하지 않음

        Vector2 direction = playerTransform.position - transform.position;


        // 공격 범위 밖에 있고 플레이어가 이동 중이라면 따라가기
        if (!isHurt && playerRange.isWalking)
        {
            //보스 방향 설정
            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            Vector3 dir = new Vector3(direction.normalized.x, 0, 0);


            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk"); // 걷기 애니메이션
            transform.position += dir * moveSpeed * Time.deltaTime;

            // 공격 범위 안에 있을 때
            if (attackRange.isAttack && !isAttackCoolDown)
            {
                Attack();
            }
        }
        // 아무 조건에도 해당하지 않으면 Idle
        else
        {
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Walk");

            SoundManager.Instance.PlaySound(SoundManager.SoundType.BossWalk);
            animator.SetTrigger("Idle");
        }
    }

    public void Attack()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Attack"); // 공격 애니메이션
        isAttackCoolDown = true;
        SoundManager.Instance.PlaySound(SoundManager.SoundType.BossAttack);
        // 일정 시간 후 쿨타임 해제
        Invoke(nameof(ResetAttackCooldown), attackCooldownDuration);
    }

    private void ResetAttackCooldown()
    {
        isAttackCoolDown = false; // 공격 가능
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerController.DealDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= (int)damage;
        UpdateHp();

        if (hp <= nextHurtHp && !isDie)
        {
            isHurt = true;
            SoundManager.Instance.PlaySound(SoundManager.SoundType.BossHurt);
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Hurt");
            nextHurtHp -= 10;
        }
        else
        {
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Hurt");
            animator.SetTrigger("Walk");
        }

        //데미지 텍스트
        GameObject damageTxt = Instantiate(damageText);
        damageTxt.transform.position = damagePos.position;
        damageTxt.GetComponent<DamageText>().damage = damage;

        if (hp <= 0)
        {
            isDie = true;
            Die();
        }
        else
        {
            Invoke("ReturnToIdle", 1.0f);
        }
        /*        Debug.Log("Idle3");
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle"); // Idle 상태로 복귀*/

    }

    void Die()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.BossDead);
        UIController.Instance.isClear = true;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Dead");
        UIController.Instance.OnGameResultPopUp();
    }

    void ReturnToIdle()
    {
        isHurt = false;
    }

    public void EnemyDie()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<EdgeCollider2D>().enabled = false;
        Destroy(gameObject);
    }

    void UpdateHp()
    {
        hpGauge.fillAmount = hp / maxHp;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerController.DealDamage(damage);
        }
    }
}
