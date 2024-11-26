using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine;
    private Player player;


    public float speed = 5;
    public float jumpSpeed = 5;
    public float invincibilityTime = 1f;
    private float prevVx = 0;
    private float vx = 0;

    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;

    private Rigidbody2D rb;

    public Image hpGauge;

    private bool isGround;
    private bool goIdle;

    public bool isDie;
    [HideInInspector]
    public bool isAttack;
    public bool isHit = false;


    private static PlayerController instance;
    public static PlayerController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        stateMachine = new StateMachine(this);
    }
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(stateMachine.idleState);

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
                    stateMachine.TransitionTo(stateMachine.idleState);
                }
                else
                {
                    stateMachine.TransitionTo(stateMachine.runState);
                }
            }
            else
            {
                if (vx != prevVx)
                {
                    if (vx == 0)
                    {
                        stateMachine.TransitionTo(stateMachine.idleState);
                    }
                    else
                    {
                        stateMachine.TransitionTo(stateMachine.runState);
                    }
                }
                else if (goIdle)
                {
                    stateMachine.TransitionTo(stateMachine.idleState);
                    goIdle = false;
                }

            }
        }
        else
        {
            if (isGround)
            {
                stateMachine.TransitionTo(stateMachine.jumpState);
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
            stateMachine.TransitionTo(stateMachine.attack1State);
        }
        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.TransitionTo(stateMachine.attack2State);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.TransitionTo(stateMachine.bowAttackState);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //stateMachine.TransitionTo(stateMachine.right -- );
        }

    }

    //플레이어가 맞는 행위
    public void DealDamage(int damage)
    {
        if (!isHit && !isDie)
        {
            isHit = true;
            player.PlayerDamage(damage);
            //hp 게이지 닳게 하기
            hpGauge.fillAmount = (float)player.Hp / player.MaxHp;
            stateMachine.TransitionTo(stateMachine.hurtState);
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
        stateMachine.TransitionTo(stateMachine.deadState);
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemBehavior item = other.GetComponent<ItemBehavior>();

        if (item != null)
        {
            Debug.Log($"아이템 획득: {item.GetItemName()}, 타입: {item.GetItemType()}");

            switch(item.itemData.itemType)
            {
                case Items.ItemType.Coin:
                    Debug.Log("Coin을 얻었습니다.");
                    break;
                case Items.ItemType.Exp:
                    Debug.Log("Exp를 얻었습니다.");
                    break;
                case Items.ItemType.Potion:
                    Debug.Log("Potion을 얻었습니다.");
                    break;
                default:
                    Debug.LogError("알 수 없는 아이템 타입!");
                    break;
            }
        }
    }
    //애니메이션 스프라이트 문제 해결 함수
    /*    void UpY()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            rb.gravityScale = 0;
        }
        void DownY()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
            rb.gravityScale = 4;
            isGround = false;
        }*/

    void SetGoIdle()
    {
        goIdle = true;
    }
}
