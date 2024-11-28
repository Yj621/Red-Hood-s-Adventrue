using TMPro;
using UnityEngine;

public class UpgradeBehavior : MonoBehaviour
{
    private Player player;
    public UpgradeButton upgradeData;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //업그레이드
    [SerializeField] private TextMeshProUGUI levelText; //레벨
    [SerializeField] private TextMeshProUGUI costText; //비용

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
            //레벨 / cost 올리기
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
