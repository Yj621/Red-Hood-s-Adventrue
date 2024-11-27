using TMPro;
using UnityEngine;

public class UpgradeBehavior : MonoBehaviour
{
    public UpgradeButton upgradeData;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //업그레이드
    [SerializeField] private TextMeshProUGUI levelText; //레벨
    [SerializeField] private TextMeshProUGUI costText; //비용

    private void Start()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();

    }

    public void OnUpgradeButtonClick()
    {
        if(upgradeData.level < upgradeData.maxLevel)
        {
            //레벨 / cost 올리기
            upgradeData.UpgradeLevel();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();    
    }
}
