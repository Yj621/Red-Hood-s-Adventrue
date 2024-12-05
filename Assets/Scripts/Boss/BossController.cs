using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IEnemy
{
    Transform player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject damageText;
    public Image hpGauge;

    private bool isWalking = false; // 걷기 상태 플래그

    [SerializeField] private float speed = 1f;
    [SerializeField] private float hp = 200;
    [SerializeField] private float maxHp = 200;
    [SerializeField] private int damage;
    [SerializeField] private float playerDistance = 8f;
    private float moveSpeed = 1f;
    private float nextHurtHp;

    Vector2 vx;
    public Transform damagePos;


    private bool isHurt = false;

    [SerializeField] private bool isDie = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator =GetComponent<Animator>();
        nextHurtHp = hp - 10;
    }

    void Update()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 일정 거리 이내일 때만 이동
        if (distanceToPlayer <= playerDistance && isHurt==false && !isDie)
        {
            Vector2 direction = new Vector2(
                transform.position.x - player.position.x,
                transform.position.y - player.position.y
            );

            // 보스가 오른쪽 또는 왼쪽을 향하도록 FlipX 설정
            spriteRenderer.flipX = direction.x < 0;

            Vector3 dir = new Vector3(-direction.normalized.x, 0, 0);

            // 움직임 처리
            if (!isHurt && dir.magnitude > 0 && !isDie)
            {
                if (!isWalking)
                {
                    Debug.Log("Walk1");
                    animator.ResetTrigger("Idle");
                    animator.SetTrigger("Walk"); // 걷기 애니메이션
                    isWalking = true; // 걷기 상태 활성화
                }
                transform.position += dir.normalized * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (isWalking)
            {
                Debug.Log("Idle");
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle"); // Idle 애니메이션
                isWalking = false; // 걷기 상태 비활성화
            }
        }
    }



    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Instance.DealDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= (int)damage;
        UpdateHp();
        Debug.Log($"적({gameObject}) 체력 : {hp}");

        if(hp <= nextHurtHp)
        {
            isHurt = true;
           animator.SetTrigger("Hurt");
            nextHurtHp -= 10;
        }
        else
        {
           animator.SetTrigger("Walk");
        }

        //데미지 텍스트
        GameObject damageTxt = Instantiate(damageText);
        damageTxt.transform.position = damagePos.position;
        damageTxt.GetComponent<DamageText>().damage = damage;

        if (hp <= 0)
        {
            Debug.Log("Dead");
            isDie = true;
            animator.SetTrigger("Dead");

        }
        else
        {
            Debug.Log("ReturnToIdle");
            Invoke("ReturnToIdle", 1.0f);
        }
/*        Debug.Log("Idle3");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Idle"); // Idle 상태로 복귀*/

    }

    void ReturnToIdle()
    {
        isHurt = false;
    }

    public void EnemyDie()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<EdgeCollider2D>().enabled = false;
        Invoke("EnemyDie", 1.2f);
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
            PlayerController.Instance.DealDamage(damage);
        }
    }
}
