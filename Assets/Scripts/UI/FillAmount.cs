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
            DontDestroyOnLoad(gameObject); // ù ��° �ν��Ͻ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ������ ������Ʈ ����
            return;
        }
    }

    public void CoolTimeStart()
    {
        if (skillImage == null)
        {
            // "Hp_Fill"�̶�� �̸��� GameObject�� ã�� Image ������Ʈ�� ������
            GameObject SkillFillObject = GameObject.Find("Skill_Fill");
            if (SkillFillObject != null)
            {
                skillImage = SkillFillObject.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("Skill_Fill GameObject�� ã�� �� �����ϴ�.");
                return;
            }
        }
        StartCoroutine(CoolTime(3f));
    }
    //��Ÿ��
    IEnumerator CoolTime(float cool)
    {
        isCooltime = true; // ��Ÿ�� ���� -> RŰ ��Ȱ��ȭ
        while (cool > 0f)
        {
            cool -= Time.deltaTime;
            skillImage.fillAmount = cool / 3f; // UI ���� (1�� �������� ���)
            yield return null; // ���� �����ӱ��� ���
        }
        isCooltime = false; // ��Ÿ�� ���� -> RŰ Ȱ��ȭ
        skillImage.fillAmount = 1f; // ��Ÿ�� ������ ��ų �̹��� �ʱ�ȭ
    }

}