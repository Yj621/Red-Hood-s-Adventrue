using UnityEngine;

public class Arrow : MonoBehaviour
{   
    public Vector2 Velocity = new Vector2(10, 0);
    private float Damage = 2f;
    void Start()
    {
        
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
