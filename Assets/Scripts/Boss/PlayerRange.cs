using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    Collider2D playerRange;
    public bool isWalking = false;
    void Start()
    {
        playerRange = GetComponent<Collider2D>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("isWalking Player");
            isWalking = true;
            //Boss Attack �ִϸ��̼� ����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isWalking = false;
        }
    }
}
