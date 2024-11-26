using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    public class ItemDrop
    {
        public Items item; //드롭될 아이템
        public int amount; //드롭 개수
        public float dropRate; //드롭 확률
    }

    public List<ItemDrop> dropItems;
    public Transform dropItemPos;

    public float speed = 1f;
    public int hp = 1;
    public int damage;
    Vector2 vx;

    public Collider2D FrontCollider;
    public Collider2D FrontBottomCollider;
    public CompositeCollider2D TerrainCollider;


    void Start()
    {
        vx = Vector2.left * speed;
        Debug.Log("적 체력 : " + hp);
    }
    void Update()
    {
        if (FrontCollider.IsTouching(TerrainCollider) || !FrontBottomCollider.IsTouching(TerrainCollider))
        //벽이 있거나 || (절벽이 있는 경우) 바닥이 없는 경우
        {
            vx = -vx; //좌우반전
            transform.localScale = new Vector2(-transform.localScale.x, 1);

        }
    }

    private void FixedUpdate()
    {
        transform.Translate(vx * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Instance.DealDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"적({gameObject}) 체력 : {hp}");
        GetComponent<Animator>().SetTrigger("Hurt");
        StartCoroutine(ReturnToIdle()); // Idle로 복귀하는 코루틴 실행

        if (hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("Dead");
            Destroy(gameObject);
            DropItems();
        }
    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(0.75f); // Hurt 애니메이션 재생 후 대기 시간
        GetComponent<Animator>().SetTrigger("Idle"); // Idle 상태로 복귀
    }

    void DropItems()
    {
        float offset = 0.5f; // 아이템 간의 간격
        float direction = 1; // 왼쪽(-1) 또는 오른쪽(1)으로 번갈아 가며 드롭
        foreach (var drop in dropItems)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= drop.dropRate)
            {
                //설정된 개수만큼 드롭
                for (int i = 0; i < drop.amount; i++)
                {                
                    // 아이템의 생성 위치를 계산
                    Vector3 dropPosition = dropItemPos.position + new Vector3(offset * i * direction, 0, 0);
                    GameObject droppedItem = Instantiate(drop.item.itemPrefab, dropPosition, Quaternion.identity);

                    Debug.Log($"{drop.item.itemName}을(를) 드롭했습니다. 위치: {dropPosition}");

                    // 방향을 번갈아 가면서 조정 (왼쪽-오른쪽)
                    direction *= -0.55f;
                }
            }
        }
    }


}
