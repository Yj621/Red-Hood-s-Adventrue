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
        //player 데이터 초기화 (Hp, Damage, Exp)
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

        //땅에 닿아있는가
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

        //플레이어 발이 바닥 콜라이더와 닿았다면
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

    //플레이어가 맞는 행위
    public void DealDamage(int damage)
    {
        if (!isHit && !isDie)
        {
            isHit = true;
            player.PlayerDamage(damage);
            Debug.Log("플레이어가 맞음");
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

    //플레이어가 죽음
    void Die()
    {
        GetComponent<Animator>().SetTrigger("Dead");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
    }

    //플레이어가 때리는 행위
    public void Attack(EnemyController enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(player.Damage);
            Debug.Log($"{player.Damage}만큼 적이 피해를 입음");
        }
        Invoke("IsAttackTrue", 1f);
    }

    void IsAttackTrue()
    {
        isAttack = false;
    }


    //애니메이션 스프라이트 문제 해결 함수
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
