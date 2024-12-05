using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform likedPortal;
    [SerializeField] private float cooldownTime = 1.0f;
    private bool isOnCooldown = false;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isOnCooldown)
        {
            //����� ��Ż�� �̵�
            TeleportPlayer(other.transform);
            //��Ÿ�� Ȱ��ȭ
            StartCoroutine(StartCooldown(likedPortal.GetComponent<Portal>()));
        }
    }

    private void TeleportPlayer(Transform player)
    {
         player.position = likedPortal.position;
    }

    IEnumerator StartCooldown(Portal otherPortal)
    {
        SetPortalState(true, 0.2f);
        otherPortal.SetPortalState(true, 0.2f); //����� ��Ż�� ��Ÿ�� ����


        yield return new WaitForSeconds(cooldownTime); //��Ÿ�ӵ��� ���

        SetPortalState(false, 1f);
        otherPortal.SetPortalState(false, 1f); //����� ��Ż�� ��Ÿ�� ����
    }

    private void SetPortalState(bool cooldownState, float alpha)
    {
        isOnCooldown = cooldownState;
        sprite.color = new Color(1,1,1,alpha);
    }
}
