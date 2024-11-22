using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 5;
    public float invincibilityTime = 1f;

    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;

    private Rigidbody2D rb;
    private Player player;

    private float prevVx = 0;
    private float vx = 0;
    private bool isGround;
    private bool goIdle;

    public bool isDie;
    [HideInInspector]
    public bool isAttack;
    public bool isHit=false;


    private static PlayerController instance;
    public static PlayerController Instance
    {
        get { return instance; }
    }
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        //player ������ �ʱ�ȭ (Hp, Damage, Exp)
        player = new Player(30, 5, 0);
    }

    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal") * speed;
        float vy = GetComponent<Rigidbody2D>().linearVelocityY;

        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //���� ����ִ°�
        if (bottomCollider.IsTouching(terrainCollider))
        {
            if (!isGround)
            {
                if (vx == 0)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Walk");
                }
            }
            else
            {
                if (vx != prevVx)
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Walk");
                    }
                }
                else if (goIdle)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                    goIdle = false;
                }

            }
        }
        else
        {
            if (isGround)
            {
                GetComponent<Animator>().SetTrigger("Jump");
            }
        }

        //�÷��̾� ���� �ٴ� �ݶ��̴��� ��Ҵٸ�
        isGround = bottomCollider.IsTouching(terrainCollider);

        if (Input.GetButtonDown("Jump") && isGround == true)
        {
            rb.gravityScale = 4f;
            vy = jumpSpeed;
        }
        prevVx = vx;

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetTrigger("Attack1");
        }
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Animator>().SetTrigger("Attack2");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Animator>().SetTrigger("BowAttack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Animator>().SetTrigger("RightAttack");
        }

    }

    //�÷��̾ �´� ����
    public void DealDamage(int damage)
    {
        if (!isHit && !isDie)
        {
            isHit = true;
            player.PlayerDamage(damage);
            Debug.Log("�÷��̾ ����");
            GetComponent<Animator>().SetTrigger("Hurt");
            Invoke("Invincibility", invincibilityTime);
        }
        if (!player.IsAlive() && isDie == false)
        {
            Die();
            isDie = true;
        }
    }
    void Invincibility()
    {
        isHit = false;
    }

    //�÷��̾ ����
    void Die()
    {
        GetComponent<Animator>().SetTrigger("Dead");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
    }

    //�÷��̾ ������ ����
    public void Attack(EnemyController enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(player.Damage);
            Debug.Log($"{player.Damage}��ŭ ���� ���ظ� ����");
        }
        Invoke("IsAttackTrue", 1f);
    }

    void IsAttackTrue()
    {
        isAttack = false;
    }


    //�ִϸ��̼� ��������Ʈ ���� �ذ� �Լ�
    void UpY()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        rb.gravityScale = 0;
    }
    void DownY()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        rb.gravityScale = 4;
        isGround = false;
    }

    void SetGoIdle()
    {
        goIdle = true;
    }
}
