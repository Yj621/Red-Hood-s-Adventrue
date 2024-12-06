using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public GameObject noticePopUp;
    public GameObject GameResultPopUp;

    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI skillPoint;
    public TextMeshProUGUI noticeText;
    public List<TextMeshProUGUI> stateText;

    public GameObject[] upButton;

    public bool isClear = false;

    private static UIController instance;
    public static UIController Instance
    { get { return instance; }}

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        instance = this;
 
    }


    void Update()
    {
        
    }


    public void UpdateCoinUI(int coins)
    {
        stateText[0].text = coins.ToString();
    }
    public void UpdateExpUI(int currentExp, int maxExp)
    {
        stateText[1].text = $"{currentExp}/{maxExp}";
    }
    public void UpdateLevelUI(int lv)
    {
        stateText[2].text = lv.ToString();
    }

    //스킬포인트 ui 업데이트
    public void UpdateSkillPointUI(int skillPoints)
    {
        skillPoint.text = skillPoints.ToString();
    }
    public void LevelUpButtonActive()
    {
        upButton[0].SetActive(true);    
    }
    public void AbilityUpButtonActive()
    {
        upButton[1].SetActive(true);
    }
    public void AbilityUpButtonDeactive()
    {
        upButton[1].SetActive(false);    
    }

    public void SetActiveNoticePopUp()
    {
        noticePopUp.SetActive(true);
    }

    public void OnClickCloseGameResultPanel()
    {
        GameResultPopUp.SetActive(false);
    }

    public void OnGameResultPopUp()
    {
        GameResultPopUp.SetActive(true);
        if (isClear)
        {
            gameResultText.text = "GAME CLEAR";
        }
        else
        {
            gameResultText.text = "GAME OVER";
        }
    }

    public void OnClickRestart()
    {
        GameManager.Instance.playerController.Restart();
    }

    public void OnClickQuit()
    {
        Application.Quit();

    }

    // public void ResetUpButton()
    // {
    //     for(int i =0; i<upButton.Length;i++)
    //     {
    //         upButton[i].SetActive(false);
    //     }
    // }

}
