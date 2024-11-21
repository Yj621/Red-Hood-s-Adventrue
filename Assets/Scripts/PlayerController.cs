using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 5;
    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;

    private Rigidbody2D rb;
    private Player player;

    private float prevVx = 0;
    private float vx = 0;
    private bool isGround;
    private bool goIdle;
    public bool isAttack;
    
    
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
        player = new Player(100, 5, 0);
    }

    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal") * speed;
        float vy = GetComponent<Rigidbody2D>().linearVelocityY;

        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Debug.Log("FLIP");
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
                else if(goIdle)
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

    public void DealDamage(int damage) 
    {
        player.PlayerDamage(damage);
        Debug.Log($"Dealt {player.Damage * damage} damage!");

        if(!player.IsAlive())
        {
            //�÷��̾� die
        }
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
