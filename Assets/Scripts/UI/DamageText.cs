using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float alphaSpeed;
    public float moveSpeed;
    public float destroyTime;
    TextMeshPro text;
    Color alpha;
    public float damage;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text =  damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0,Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }
    private void DestroyObject()
    {
        Destroy(gameObject);    
    }
}
