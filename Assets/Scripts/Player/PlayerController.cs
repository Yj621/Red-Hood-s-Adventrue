using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    StateMachine stateMachine;
    GameManager gm;
    Weapon weapon;
    [SerializeField] private EnemyController Enemy;

    public GameObject ArrowPos;
    public Collider2D bottomCollider;
    private Rigidbody2D rb;
    public Image hpGauge;

    Vector3 originalPos;

    public bool isDie;
    private bool isSliding = false;
    private bool isGround;
    private bool goIdle;
    public bool isCut = false;
    public bool isSeriesCut = false;
    [HideInInspector]
    public bool isAttack;
    public bool isHit = false;

    private static PlayerController instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 첫 번째 인스턴스만 유지
        }
        else
        {
            Destroy(gameObject); // 중복 생성된 오브젝트 삭제
            return;
        }
        stateMachine = new StateMachine(this);
    }
    void Start()
    {
        //   gm = GameManager.Instance;
        weapon = GameManager.Instance.player.weapon;

        originalPos = new Vector3(-6, 0, 0);
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(stateMachine.idleState);

        UIController.Instance.UpdateCoinUI(GameManager.Instance.player.Coins);
        Debug.Log(GameManager.Instance.player.Hp);

        if (hpGauge == null)
        {
            // "Hp_Fill"이라는 이름의 GameObject를 찾아 Image 컴포넌트를 가져옴
            GameObject hpFillObject = GameObject.Find("Hp_Fill");
            if (hpFillObject != null)
            {
                hpGauge = hpFillObject.GetComponent<Image>();
                Debug.Log("찾음");
            }
            else
            {
                Debug.LogError("Hp_Fill GameObject를 찾을 수 없습니다.");
                return;
            }
        }
    }

    void Update()
    {
        GameManager.Instance.player.Vx = Input.GetAxisRaw("Horizontal") * GameManager.Instance.player.Speed;
        float vy = GetComponent<Rigidbody2D>().linearVelocityY;

        if (GameManager.Instance.player.Vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (GameManager.Instance.player.Vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // 땅에 닿아있는가
        if (IsGrounded())
        {
            if (!isGround)
            {
                if (GameManager.Instance.player.Vx == 0)
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
                if (GameManager.Instance.player.Vx != GameManager.Instance.player.PrevVx)
                {
                    if (GameManager.Instance.player.Vx == 0)
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
            vy = GameManager.Instance.player.JumpSpeed;
        }
        GameManager.Instance.player.PrevVx = GameManager.Instance.player.Vx;

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GameManager.Instance.player.Vx, vy);


        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GameManager.Instance.player.Vx, vy);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            stateMachine.TransitionTo(stateMachine.attack1State);
            isCut = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && FillAmount.Instance.isCooltime == false)
        {
            stateMachine.TransitionTo(stateMachine.attack2State);
            isSeriesCut = true;
            //쿨타임 시작
            FillAmount.Instance.CoolTimeStart();
        }
        if (Input.GetMouseButtonDown(1) && stateMachine.CurrentState != stateMachine.runState)
        {
            stateMachine.TransitionTo(stateMachine.bowAttackState);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(GameManager.Instance.player.Vx) > 0 && isGround && !isSliding)
        {
            StartSlide();
        }

        if (GameManager.Instance.player.Coins >= 10)
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
            GameManager.Instance.player.GetDamage(damage);
            //hp 게이지 닳게 하기
            UpdateHp();
            StartCoroutine(HurtColor(0.5f));
            Invoke("Invincibility", GameManager.Instance.player.InvincibilityTime);
        }

        //플레이어 죽기
        if (!GameManager.Instance.player.IsAlive() && isDie == false)
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
        UIController.Instance.isClear = false;
        Debug.Log("Player Dead");
        stateMachine.TransitionTo(stateMachine.deadState);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //enabled = false;
        GameManager.Instance.CameraOff();
        UIController.Instance.OnGameResultPopUp();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        // DontDestroyOnLoad로 설정된 모든 객체를 찾아서 삭제
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("DontDestroy"))  // 'DontDestroy' 태그가 설정된 객체만 삭제
            {
                Destroy(obj);  // 해당 객체 삭제
            }
        }

        // GameScene 씬을 로드
        SceneManager.LoadScene("GameScene");  // 0번째 GameScene 씬을 로드
        Time.timeScale = 1f;
    }


    //기본 공격
    public void CutAttack(IEnemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(weapon.cutDamage);
        }
        Invoke("IsAttackTrue", 1f);
    }

    //연속 베기
    public void SeriesCutAttack(IEnemy enemy)
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
            string logMessage = "";

            switch (item.itemData.itemType)
            {
                case Items.ItemType.Coin:
                    GameManager.Instance.player.SetCoins(Enemy.dropItems[0].itemPrice, "Up");
                    logMessage = $"Coin을 {Enemy.dropItems[0].itemPrice}개 얻었습니다.";
                    break;

                case Items.ItemType.Exp:
                    GameManager.Instance.player.GetExperience(Enemy.dropItems[1].itemPrice);
                    logMessage = $"Exp을 {Enemy.dropItems[1].itemPrice}개 얻었습니다.";
                    break;

                case Items.ItemType.Potion:
                    GameManager.Instance.player.Heal(Enemy.dropItems[2].itemPrice);
                    logMessage = $"Potion을 얻어 {Enemy.dropItems[2].itemPrice}만큼 회복했습니다.";
                    UpdateHp();
                    break;

                default:
                    Debug.LogError("알 수 없는 아이템 타입!");
                    break;
            }
            ChatManager.Instance.AddMessage(logMessage);
            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == "GameManager.Instance.player")
        {
            Die();
            GameManager.Instance.CameraOff();
            Debug.Log("Die");
        }

        if (other.gameObject.tag == "BossPortal")
        {
            FindObjectOfType<FadeController>().FadeOutAndLoadScene(1); // 전환할 씬의 인덱스
        }
    }
    void UpdateHp()
    {
        // Hp 게이지 업데이트
        hpGauge.fillAmount = (float)GameManager.Instance.player.Hp / GameManager.Instance.player.MaxHp;
    }

    //땅 위에 있는지 확인
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

    void SetGoIdle()
    {
        goIdle = true;
        isSeriesCut = false;
        isCut = false;
    }


    //Hurt
    IEnumerator HurtColor(float cool)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color orignalColor = spriteRenderer.color;

        int flashCount = 4;
        for (int i = 0; i < flashCount; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.8f);

            yield return new WaitForSeconds(cool / (flashCount * 2));
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(cool / (flashCount * 2));
        }
        spriteRenderer.color = orignalColor;
    }

    //슬라이딩
    void StartSlide()
    {
        isSliding = true;
        GameManager.Instance.player.SlideTimer = 0f;

        // 슬라이드 방향 설정
        float slideDirection = GameManager.Instance.player.Vx > 0 ? 1 : -1; // vx가 양수면 오른쪽, 음수면 왼쪽

        // 슬라이드 속도 적용
        rb.linearVelocity = new Vector2(slideDirection * GameManager.Instance.player.SlideSpeed, rb.linearVelocity.y);

        // 슬라이드 애니메이션 전환
        stateMachine.TransitionTo(stateMachine.slideState);

        // 슬라이드 종료를 위한 코루틴 시작
        StartCoroutine(SlideCoroutine());
    }

    IEnumerator SlideCoroutine()
    {
        while (GameManager.Instance.player.SlideTimer < GameManager.Instance.player.SlideDuration)
        {
            GameManager.Instance.player.SlideTimer += Time.deltaTime;
            yield return null;
        }

        EndSlide();
    }



    void EndSlide()
    {
        isSliding = false;

        // 슬라이드 종료 후 정지
        rb.linearVelocity = Vector2.zero;

        // 슬라이드 애니메이션 종료 후 상태 전환
        if (GameManager.Instance.player.Vx == 0)
        {
            stateMachine.TransitionTo(stateMachine.idleState);
        }
        else
        {
            stateMachine.TransitionTo(stateMachine.runState);
        }
    }


}
