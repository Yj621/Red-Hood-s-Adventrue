using UnityEngine;

public class BossController : MonoBehaviour
{
    Transform player;
    private int rotateSpeed = 2;
    private float moveSpeed = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 direction = new Vector2(
            transform.position.x - player.position.x,
            transform.position.y - player.position.y
            );
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed);

        transform.rotation = rotation;

        Vector3 dir = direction;
        transform.position += (-dir.normalized * moveSpeed * Time.deltaTime);
    }
}
