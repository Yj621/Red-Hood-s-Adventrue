using UnityEngine;

public class Arrow : MonoBehaviour
{   
    public Vector2 Velocity = new Vector2(10, 0);
    private float Damage;
    private static Arrow instance;
    public static Arrow Instance
    {
        get { return instance; }
    }
    void Start()
    {
        Damage = PlayerController.Instance.weapon.bowDamage;
        instance = this;
    }

    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().MovePosition(
            GetComponent<Rigidbody2D>().position + Velocity * Time.fixedDeltaTime);
    }
    void Update()
    {
        if(!GetComponent<SpriteRenderer>().isVisible)
        {
            gameObject.SetActive(false);
        }    
        
        // bowDamage와 Damage가 다르면 업데이트
        if (Damage != PlayerController.Instance.weapon.bowDamage)
        {
            Damage = PlayerController.Instance.weapon.bowDamage;
            Debug.Log($"Damage updated: {Damage}");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            other.GetComponent<EnemyController>().TakeDamage(Damage);
        }
    }
}
