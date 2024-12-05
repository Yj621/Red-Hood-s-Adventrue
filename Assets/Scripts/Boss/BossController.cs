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
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ���� �Ÿ� �̳��� ���� �̵�
        if (distanceToPlayer <= 5f) // 5f�� �̵��� �����ϴ� �Ÿ��� �ʿ信 ���� ����
        {
            Vector2 direction = new Vector2(
                transform.position.x - player.position.x,
                transform.position.y - player.position.y
            );

            // ������ ������ �Ǵ� ������ ���ϵ��� FlipX ����
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true; // ���������� ������ �� FlipX ����
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false; // �������� ������ �� FlipX ����
            }

            Vector3 dir = new Vector3(-direction.normalized.x, 0, 0);

            // ������ ó��
            if (!isHurt && dir.magnitude > 0)
            {
                Debug.Log("Walk");
                animator.SetTrigger("Walk"); // �ȱ� �ִϸ��̼�
                transform.position += dir.normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("Idle"); // Idle ���·� ����
            }
        }
        else
        {
            // �÷��̾ ���� �Ÿ� �ۿ� ���� ���� Idle ���·� ��ȯ
            animator.SetTrigger("Idle");
        }
    }



    private void FixedUpdate()
    {
        //�¾����� ������ �ֵ��� �ϱ�
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
        Debug.Log($"��({gameObject}) ü�� : {hp}");
        //������ �ؽ�Ʈ
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
        GetComponent<Animator>().SetTrigger("Idle"); // Idle ���·� ����

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
