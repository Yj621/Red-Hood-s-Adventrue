using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public int hp;
    public int damage;
    Vector2 vx;

    public Collider2D FrontCollider;
    public Collider2D FrontBottomCollider;
    public CompositeCollider2D TerrainCollider;
    void Start()
    {
        vx = Vector2.left * speed;

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
        transform.Translate(vx*Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag ("Player"))
        {
            PlayerController.Instance.DealDamage(damage);
            Debug.Log($"Player가 {gameObject.name}와 충돌하여 {damage} 데미지를 받음!");

        }
    }
}
