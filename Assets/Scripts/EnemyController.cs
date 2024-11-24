using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
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
        }
    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(0.75f); // Hurt 애니메이션 재생 후 대기 시간
        GetComponent<Animator>().SetTrigger("Idle"); // Idle 상태로 복귀
    }

}
