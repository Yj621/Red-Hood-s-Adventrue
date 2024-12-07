using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
    public float animSpeed = 1.0f;
    public Animator animator;
    private static AnimationSpeed instance;
    public static AnimationSpeed Instance
    {
        get { return instance; }
    }
    void Awake()
    {

        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        instance = this;
    }
    private void Start()
    {
    }

    public void PlusAnimationSpeed()
    {
        animSpeed += 0.05f;
        GameManager.Instance.player.AnimationSpeed -= 0.05f;
    }
    
    void Update()
    {
        if (animator == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                animator = player.GetComponent<Animator>();
            }
            else
            {
                Debug.LogError("player GameObject�� ã�� �� �����ϴ�.");
                return;
            }
        }
        else
        {
            animator.SetFloat("AttackSpeed", animSpeed);
        }
    }
}
