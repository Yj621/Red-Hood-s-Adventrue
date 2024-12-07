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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 첫 번째 인스턴스만 유지
        }
        else
        {
            Destroy(gameObject); // 중복 생성된 오브젝트 삭제
            return;
        }
    }

    public void CoolTimeStart()
    {
        if (skillImage == null)
        {
            // "Hp_Fill"이라는 이름의 GameObject를 찾아 Image 컴포넌트를 가져옴
            GameObject SkillFillObject = GameObject.Find("Skill_Fill");
            if (SkillFillObject != null)
            {
                skillImage = SkillFillObject.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("Skill_Fill GameObject를 찾을 수 없습니다.");
                return;
            }
        }
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