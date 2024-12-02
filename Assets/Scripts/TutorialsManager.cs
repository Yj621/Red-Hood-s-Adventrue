using UnityEngine;

public class TutorialsManager : MonoBehaviour
{
    public RectTransform ButtonTransform; // 버튼 위치를 설정하는 RectTransform
    public Vector2[] ButtonPosition; // 버튼의 위치를 저장하는 배열
    public GameObject[] Tutorials; // Tutorials 오브젝트 배열
    public GameObject[] TextBoxs; // TextBox 오브젝트 배열

    private int currentTutorial = 0; // 현재 Tutorials 인덱스
    private int currentTextBox = 0; // 현재 TextBox 인덱스

    void Start()
    {
        if (Tutorials.Length > 0)
        {
            Tutorials[0].SetActive(true); // 처음 시작할 때 첫 Tutorials 활성화
        }
    }

    public void CheckButton()
    {
        Debug.Log("ButtonPosition : "+ButtonPosition);
           
        // 이전 TextBox 비활성화
        if (currentTextBox > 0 && currentTextBox <= TextBoxs.Length)
        {
            TextBoxs[currentTextBox - 1].SetActive(false);
        }

        // TextBoxs를 순서대로 활성화
        if (currentTextBox < TextBoxs.Length)
        {
            TextBoxs[currentTextBox].SetActive(true);

            // ButtonPosition에 따라 버튼 위치 설정
            if (currentTextBox < ButtonPosition.Length)
            {
                ButtonTransform.anchoredPosition = ButtonPosition[currentTextBox];
            }

            currentTextBox++;
            return; // Tutorials 전환 로직으로 넘어가지 않음
        }

        // 모든 TextBoxs가 활성화된 후 Tutorials 변경
        if (currentTutorial < Tutorials.Length)
        {
            Tutorials[currentTutorial].SetActive(false); // 현재 Tutorials 비활성화
        }

        currentTutorial++;

        if (currentTutorial < Tutorials.Length)
        {
            Tutorials[currentTutorial].SetActive(true); // 다음 Tutorials 활성화

            // 버튼 위치를 Tutorials의 위치로 이동
            if (currentTutorial < ButtonPosition.Length)
            {
                ButtonTransform.anchoredPosition = ButtonPosition[currentTutorial];
            }
        }

        // TextBox 인덱스 초기화 (다음 Tutorials에서 새 TextBox를 사용할 준비)
        currentTextBox = 0;
    }
}
