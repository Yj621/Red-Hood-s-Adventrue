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
        instance = this;
    }

    public void PlusAnimationSpeed()
    {
        animSpeed += 0.05f;
    }
    
    void Update()
    {
        animator.SetFloat("AttackSpeed", animSpeed);
    }
}
