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
            //연결된 포탈로 이동
            TeleportPlayer(other.transform);
            //쿨타임 활성화
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
        otherPortal.SetPortalState(true, 0.2f); //연결된 포탈도 쿨타임 시작


        yield return new WaitForSeconds(cooldownTime); //쿨타임동안 대기

        SetPortalState(false, 1f);
        otherPortal.SetPortalState(false, 1f); //연결된 포탈도 쿨타임 시작
    }

    private void SetPortalState(bool cooldownState, float alpha)
    {
        isOnCooldown = cooldownState;
        sprite.color = new Color(1,1,1,alpha);
    }
}
