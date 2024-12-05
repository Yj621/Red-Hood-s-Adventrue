using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform likedPortal;
    public float cooldownTime = 1.0f;
    private bool isOnCooldown = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isOnCooldown)
        {
            //����� ��Ż�� �̵�
            other.transform.position = likedPortal.position;
            //��Ÿ�� Ȱ��ȭ
            StartCoroutine(StartCooldown(likedPortal.GetComponent<Portal>()));
        }
    }
    IEnumerator StartCooldown(Portal otherPortal)
    {
        isOnCooldown = true; //���� ��Ż ��Ÿ�� ����
        otherPortal.isOnCooldown = true; //����� ��Ż�� ��Ÿ�� ����

        yield return new WaitForSeconds(cooldownTime); //��Ÿ�ӵ��� ���

        isOnCooldown = false; //��Ÿ�� ����
        otherPortal.isOnCooldown = false; //����� ��Ż ��Ÿ�� ����
    }
}
