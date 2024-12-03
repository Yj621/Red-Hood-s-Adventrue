using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine;
    public Player player;
    public Weapon weapon;
    [SerializeField] private EnemyController Enemy;

    public float speed = 5;
    public float jumpSpeed = 5;
    public float invincibilityTime = 1f;
    public float bowDamage = 2f;
    private float prevVx = 0;
    private float vx = 0;
   

    public GameObject ArrowPos;
    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;
    private Rigidbody2D rb;
    public Image hpGauge;

    Vector2 originalPos;

    public bool isDie;
    private bool isGround;
    private bool goIdle;
    public bool isCut = false;
    public bool isSeriesCut = false;
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
        //player 데이터 초기화 (Hp, Damage, Exp, Coins)
        player = new Player(100, 5, 0, 10);
        weapon = new Weapon();
    }
    void Start()
    {
        instance = this;
        originalPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(stateMachine.idleState);

        UIController.Instance.UpdateCoinUI(player.Coins);
        Debug.Log(player.Hp);
        Debug.Log("weapon.cutDamage" + weapon.cutDamage);
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

        // 땅에 닿아있는가
        if (IsGrounded())
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

        isGround = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rb.gravityScale = 4f;
            vy = jumpSpeed;
        }
        prevVx = vx;

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);


        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            stateMachine.TransitionTo(stateMachine.attack1State);
            isCut = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.TransitionTo(stateMachine.attack2State);
            isSeriesCut = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.TransitionTo(stateMachine.bowAttackState);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //stateMachine.TransitionTo(stateMachine.right -- );
        }

        if (player.Coins >= 10)
        {
            UIController.Instance.AbilityUpButtonActive();
        }
        else
        {
            UIController.Instance.AbilityUpButtonDeactive();
        }
    }

    public void BowAttack()
    {
        Vector2 arrowV = new Vector2(10, 0);
        if (GetComponent<SpriteRenderer>().flipX)
        {
            arrowV.x = -arrowV.x;
        }
        GameObject arrow = GameManager.Instance.ArrowPool.GetObject();
        arrow.transform.position = ArrowPos.transform.position;
        arrow.GetComponent<Arrow>().Velocity = arrowV;
    }


    //플레이어가 맞는 행위
    public void DealDamage(int damage)
    {
        if (!isHit && !isDie)
        {
            isHit = true;
            player.GetDamage(damage);
            //hp 게이지 닳게 하기
            UpdateHp();
            stateMachine.TransitionTo(stateMachine.hurtState);
            Invoke("Invincibility", invincibilityTime);
        }

        //플레이어 죽기
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
    public void Die()
    {
        stateMachine.TransitionTo(stateMachine.deadState);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //enabled = false;
        GameManager.Instance.CameraOff();
        Invoke("Restart", 2);
    }

    //재시작
    public void Restart()
    {
        // 현재 씬 다시 불러오기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //기본 공격
    public void CutAttack(EnemyController enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(weapon.cutDamage);
        }
        Invoke("IsAttackTrue", 1f);
    }

    //연속 베기
    public void SeriesCutAttack(EnemyController enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(weapon.seriesCutDamage);
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

            switch (item.itemData.itemType)
            {
                case Items.ItemType.Coin:
                    player.SetCoins(Enemy.dropItems[0].itemPrice, "Up");
                    Debug.Log("Coin을 얻었습니다. : " + Enemy.dropItems[0].itemPrice);
                    Destroy(other.gameObject);
                    break;
                case Items.ItemType.Exp:
                    player.GetExperience(Enemy.dropItems[1].itemPrice);
                    Destroy(other.gameObject);
                    Debug.Log("Exp를 얻었습니다. : " + Enemy.dropItems[1].itemPrice);
                    break;
                case Items.ItemType.Potion:
                    player.Heal(Enemy.dropItems[2].itemPrice);
                    Debug.Log("Potion을 얻었습니다.");
                    UpdateHp();
                    Destroy(other.gameObject);
                    break;
                default:
                    Debug.LogError("알 수 없는 아이템 타입!");
                    break;
            }
        }

        if (other.gameObject.tag == "Player")
        {
            Die();
            GameManager.Instance.CameraOff();
            Debug.Log("Die");
        }
    }

    void SetGoIdle()
    {
        goIdle = true;
        isSeriesCut = false;
        isCut = false;
    }

    void UpdateHp()
    {
        hpGauge.fillAmount = (float)player.Hp / player.MaxHp;
    }
    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bottomCollider.bounds.center, bottomCollider.bounds.extents.y + 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Ground"))  // Ground 태그 확인
            {
                return true;
            }
        }
        return false;
    }
}
