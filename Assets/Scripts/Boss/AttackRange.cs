using UnityEngine;

public class AttackRange : MonoBehaviour
{
    Collider2D attackRange;
    public bool isAttack = false;
    void Start()
    {
        attackRange = GetComponent<Collider2D>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isAttack = true; 
            //Boss Attack �ִϸ��̼� ����
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = false;
        }

    }
}
