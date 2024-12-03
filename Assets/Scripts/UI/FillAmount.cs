using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount : MonoBehaviour
{
    public Image skillImage;
    public bool isCooltime;
    private static FillAmount instance;
    public static FillAmount Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    public void CoolTimeStart()
    {
        StartCoroutine(CoolTime(3f));
    }
    //쿨타임
    IEnumerator CoolTime(float cool)
    {
        isCooltime = true; // 쿨타임 시작 -> R키 비활성화
        while (cool > 0f)
        {
            cool -= Time.deltaTime;
            skillImage.fillAmount = cool / 3f; // UI 갱신 (1초 기준으로 계산)
            yield return null; // 다음 프레임까지 대기
        }
        isCooltime = false; // 쿨타임 종료 -> R키 활성화
        skillImage.fillAmount = 1f; // 쿨타임 끝나면 스킬 이미지 초기화
    }

}
