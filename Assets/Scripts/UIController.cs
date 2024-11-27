using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public List<TextMeshProUGUI> stateText;

    public GameObject[] upButton;

    private static UIController instance;
    public static UIController Instance
    { get { return instance; }}

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

    public void LevelUpButtonActive()
    {
        upButton[0].SetActive(true);    
    }
    public void AbilityUpButtonActive()
    {
        upButton[1].SetActive(true);    
    }
}
