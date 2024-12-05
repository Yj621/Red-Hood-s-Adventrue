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
            //연결된 포탈로 이동
            other.transform.position = likedPortal.position;
            //쿨타임 활성화
            StartCoroutine(StartCooldown(likedPortal.GetComponent<Portal>()));
        }
    }
    IEnumerator StartCooldown(Portal otherPortal)
    {
        isOnCooldown = true; //현재 포탈 쿨타임 시작
        otherPortal.isOnCooldown = true; //연결된 포탈도 쿨타임 시작

        yield return new WaitForSeconds(cooldownTime); //쿨타임동안 대기

        isOnCooldown = false; //쿨타임 해제
        otherPortal.isOnCooldown = false; //연결된 포탈 쿨타임 해제
    }
}
