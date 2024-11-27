using TMPro;
using UnityEngine;

public class UpgradeBehavior : MonoBehaviour
{
    public UpgradeButton upgradeData;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //���׷��̵�
    [SerializeField] private TextMeshProUGUI levelText; //����
    [SerializeField] private TextMeshProUGUI costText; //���

    private void Start()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();

    }

    public void OnUpgradeButtonClick()
    {
        if(upgradeData.level < upgradeData.maxLevel)
        {
            //���� / cost �ø���
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
