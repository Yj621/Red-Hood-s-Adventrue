using UnityEngine;

public class TutorialsManager : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public RectTransform ButtonTransform; // 버튼 위치를 설정하는 RectTransform
    public Vector2[] ButtonPosition; // 버튼의 위치를 저장하는 배열
    public GameObject[] Tutorials; // Tutorials 오브젝트 배열
    public GameObject[] TextBoxs; // TextBox 오브젝트 배열

    private int currentTutorial = 0; // 현재 Tutorials 인덱스
    private int currentTextBox = 0; // 현재 TextBox 인덱스
    private int buttonPositionIndex = 0; // ButtonPosition의 현재 인덱스

    void Start()
    {
        // 첫 번째 Tutorials와 TextBox를 활성화
        if (Tutorials.Length > 0)
        {
            Tutorials[0].SetActive(true);
        }
        if (TextBoxs.Length > 0)
        {
            TextBoxs[0].SetActive(true);
        }

        // 버튼 위치 초기화
        UpdateButtonPosition();
    }

    public void CheckButton()
    {
        Debug.Log($"CurrentTutorial: {currentTutorial}, CurrentTextBox: {currentTextBox}, ButtonIndex: {buttonPositionIndex}");

        // 현재 TextBox 비활성화
        if (currentTextBox < TextBoxs.Length)
        {
            TextBoxs[currentTextBox].SetActive(false);
        }

        // 다음 TextBox로 이동
        currentTextBox++;

        if (currentTextBox < TextBoxs.Length)
        {
            // 다음 TextBox 활성화
            TextBoxs[currentTextBox].SetActive(true);

            // 버튼 위치 갱신
            buttonPositionIndex++;
            UpdateButtonPosition();
            return; // Tutorials 전환 로직으로 넘어가지 않음
        }

        // 모든 TextBox를 처리한 경우 Tutorials 전환
        if (currentTutorial < Tutorials.Length)
        {
            // 현재 Tutorials 비활성화
            Tutorials[currentTutorial].SetActive(false);
            Debug.Log($"Deactivated Tutorial: {currentTutorial}");
        }
        
        // 다음 Tutorials로 이동
        currentTutorial++;

        if (currentTutorial < Tutorials.Length)
        {
            // 다음 Tutorials 활성화
            Tutorials[currentTutorial].SetActive(true);
            Debug.Log($"Activated Tutorial: {currentTutorial}");
        }
        else
        {
            Debug.Log("All Tutorials completed!");
        }


        // TextBox 인덱스 초기화
        currentTextBox = 0;

        // 버튼 위치 갱신
        buttonPositionIndex++;
        UpdateButtonPosition();

        // 다음 Tutorials의 첫 TextBox 활성화
        if (currentTextBox < TextBoxs.Length)
        {
            TextBoxs[currentTextBox].SetActive(true);
        }
    }

    private void UpdateButtonPosition()
    {
        // ButtonPosition에 따라 버튼 위치 설정
        if (buttonPositionIndex < ButtonPosition.Length)
        {
            ButtonTransform.anchoredPosition = ButtonPosition[buttonPositionIndex];
            Debug.Log($"ButtonPosition Updated: {ButtonPosition[buttonPositionIndex]}");
        }
        else
        {
            Debug.LogWarning("ButtonPositionIndex exceeds ButtonPosition array length!");
        }
    }
}
