using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IEnemy
{
    Transform player;
    private float moveSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject damageText;
    public Image hpGauge;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float hp = 100;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private int damage;
    Vector2 vx;
    public Transform damagePos;


    private bool isHurt = false;

    [SerializeField] private bool isDie = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 일정 거리 이내일 때만 이동
        if (distanceToPlayer <= 5f) // 5f는 이동을 시작하는 거리로 필요에 따라 조정
        {
            Vector2 direction = new Vector2(
                transform.position.x - player.position.x,
                transform.position.y - player.position.y
            );

            // 보스가 오른쪽 또는 왼쪽을 향하도록 FlipX 설정
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true; // 오른쪽으로 움직일 때 FlipX 설정
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false; // 왼쪽으로 움직일 때 FlipX 해제
            }

            Vector3 dir = new Vector3(-direction.normalized.x, 0, 0);

            // 움직임 처리
            if (!isHurt && dir.magnitude > 0)
            {
                Debug.Log("Walk");
                animator.SetTrigger("Walk"); // 걷기 애니메이션
                transform.position += dir.normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("Idle"); // Idle 상태로 복귀
            }
        }
        else
        {
            // 플레이어가 일정 거리 밖에 있을 때는 Idle 상태로 전환
            animator.SetTrigger("Idle");
        }
    }



    private void FixedUpdate()
    {
        //맞았을때 가만히 있도록 하기
        if (isHurt == false)
            transform.Translate(vx * Time.fixedDeltaTime);
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
        isHurt = true;
        hp -= (int)damage;
        Debug.Log($"적({gameObject}) 체력 : {hp}");
        //데미지 텍스트
        GameObject damageTxt = Instantiate(damageText);
        damageTxt.transform.position = damagePos.position;
        damageTxt.GetComponent<DamageText>().damage = damage;

        GetComponent<Animator>().SetTrigger("Hurt");
        if (hp <= 0)
        {
            isDie = true;
            GetComponent<Animator>().SetTrigger("Dead");
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("EnemyDie", 1.2f);
        }
        else
        {
            Invoke("ReturnToIdle", 0.75f);
        }
        GetComponent<Animator>().SetTrigger("Idle"); // Idle 상태로 복귀

    }

    void ReturnToIdle()
    {
        isHurt = false;
    }

    void EnemyDie()
    {
        Destroy(gameObject);
    }

    void UpdateHp()
    {
        hpGauge.fillAmount = hp / maxHp;
    }
}
