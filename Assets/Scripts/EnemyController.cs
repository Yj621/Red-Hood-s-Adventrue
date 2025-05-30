using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour, IEnemy
{
    [System.Serializable]
    public class ItemDrop
    {
        public Items item; //드롭될 아이템
        public int amount; //드롭 개수
        public float dropRate; //드롭 확률
        public int itemPrice; //양
    }

    public List<ItemDrop> dropItems;

    public Transform dropItemPos;
    public Transform damagePos;
    
    public GameObject damageText;

    [SerializeField] private float speed = 1f;
    [SerializeField] private int hp = 1;
    [SerializeField] private int damage;
    Vector2 vx;

    private bool isHurt = false;
    //적끼리 부딪혀도 좌우반전되도록
    private bool isTouchEnemy = false;
    [SerializeField] private bool isDie = false;

    public Collider2D FrontCollider;
    public Collider2D FrontBottomCollider;
    public CompositeCollider2D TerrainCollider;


    void Start()
    {
        vx = Vector2.left * speed;
    }
    void Update()
    {
        if (FrontCollider.IsTouching(TerrainCollider) || !FrontBottomCollider.IsTouching(TerrainCollider) || isTouchEnemy == true)
        //벽이 있거나 || (절벽이 있는 경우) 바닥이 없는 경우
        {
            vx = -vx; //좌우반전
            transform.localScale = new Vector2(-transform.localScale.x, 0.8f);
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
            GameManager.Instance.playerController.DealDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        isHurt = true;
        hp -= (int)damage;
        //데미지 텍스트
        GameObject damageTxt = Instantiate(damageText);
        damageTxt.transform.position = damagePos.position;
        damageTxt.GetComponent<DamageText>().damage = damage;

        GetComponent<Animator>().SetTrigger("Hurt");
        if (hp <= 0)
        {
            isDie = true;
            GetComponent<Animator>().SetTrigger("Dead");
            if(gameObject.name == "Boar")
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundType.BoarDead);
            }
            else if(gameObject.name == "Snail")
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundType.SnailDead);
            }
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("EnemyDie", 1.2f);
        }
        else
        {
            Invoke("ReturnToIdle", 1.5f);
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
        DropItems();
    }

    void DropItems()
    {
        float baseOffset = 0.5f; // 아이템 간의 기본 간격
        float randomOffsetRange = 0.2f; // 위치에 약간의 랜덤성을 추가하기 위한 범위
        float direction = 1; // 왼쪽(-1) 또는 오른쪽(1)으로 번갈아 가며 드롭
        HashSet<Vector3> usedPositions = new HashSet<Vector3>(); // 겹침 방지를 위한 위치 기록

        foreach (var drop in dropItems)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= drop.dropRate)
            {
                for (int i = 0; i < drop.amount; i++)
                {
                    Vector3 dropPosition;

                    do
                    {
                        // 아이템 생성 위치 계산
                        float offset = baseOffset * i + Random.Range(-randomOffsetRange, randomOffsetRange);
                        dropPosition = dropItemPos.position + new Vector3(offset * direction, 0, 0);

                    } while (usedPositions.Contains(dropPosition)); // 겹치는 위치인지 확인

                    // 위치 추가
                    usedPositions.Add(dropPosition);

                    // 아이템 생성
                    GameObject droppedItem = Instantiate(drop.item.itemPrefab, dropPosition, Quaternion.identity);

                    // 방향을 번갈아 가며 조정
                    direction *= -1;
                }
            }
        }
    }

    //적끼리 부딪히면 좌우반전하기 위해서
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isTouchEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        {
            if (other.gameObject.tag == "Enemy" && isDie == false)
            {
                isTouchEnemy = false;
            }
        }
    }

}