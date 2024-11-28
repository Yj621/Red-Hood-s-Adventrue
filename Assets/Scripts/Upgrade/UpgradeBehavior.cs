using TMPro;
using UnityEngine;

public class UpgradeBehavior : MonoBehaviour
{
    private Player player;
    public UpgradeButton upgradeData;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //���׷��̵�
    [SerializeField] private TextMeshProUGUI levelText; //����
    [SerializeField] private TextMeshProUGUI costText; //���

    private void Start()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();
        player = PlayerController.Instance.player;
    }

    public void OnCharUpgradeButtonClick()
    {
        if(upgradeData.level < upgradeData.maxLevel)
        {
            //���� / cost �ø���
            upgradeData.UpgradeLevel();
            UpdateUI();
            player.UsedCoins(upgradeData.cost);
            Debug.Log("upgradeData.cost : "+upgradeData.cost);

            switch(upgradeData.upgradeTitle)
            {
            }
        }
    }

    private void UpdateUI()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();    
    }
}
