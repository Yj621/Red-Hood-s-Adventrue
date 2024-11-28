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


            switch (upgradeData.upgradeTitle)
            {
                case "ĳ���� �̵� �ӵ�":
                    PlayerController.Instance.speed += 0.1f;
                    break;

                case "ĳ���� ���� �ӵ�":
                    PlayerController.Instance.jumpSpeed += 0.1f;
                    break;

                case "ĳ���� ���� �ð�":
                    PlayerController.Instance.invincibilityTime += 0.1f;
                    break;

                case "�⺻ ���ݷ�":
                    player.Damage += 0.1f;
                    break;

                default:
                    break;
            } 
        }
    }

    private void UpdateUI()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();    
    }
}
