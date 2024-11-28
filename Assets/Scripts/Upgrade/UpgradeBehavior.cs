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


            switch (upgradeData.upgradeTitle)
            {
                case "캐릭터 이동 속도":
                    PlayerController.Instance.speed += 0.1f;
                    break;

                case "캐릭터 점프 속도":
                    PlayerController.Instance.jumpSpeed += 0.1f;
                    break;

                case "캐릭터 무적 시간":
                    PlayerController.Instance.invincibilityTime += 0.1f;
                    break;

                case "기본 공격력":
                    player.Damage += 0.1f;
                    break;

                default:
                    break;
            } 
        }
        else
        {
            UIController.Instance.SetActiveNoticePopUp();
            Debug.Log("코인 부족");
        }
    }
    
    //스킬포인트를 사용해 업그레이드 버튼을 누르면
    public void OnSkillUpgradeButtonClick()
    {
        if(player.SkillPoints > 0)
        {
            //스킬 레벨 텍스트 업데이트
            levelText.text = upgradeData.level.ToString();
            UpdateUI();
            player.UsedSkillPoint(1);
            Debug.Log("upgradeData.cost : "+upgradeData.cost);


            switch (upgradeData.upgradeTitle)
            {
                case "캐릭터 이동 속도":
                    PlayerController.Instance.speed += 0.1f;
                    break;

                case "캐릭터 점프 속도":
                    PlayerController.Instance.jumpSpeed += 0.1f;
                    break;

                case "캐릭터 무적 시간":
                    PlayerController.Instance.invincibilityTime += 0.1f;
                    break;

                case "기본 공격력":
                    player.Damage += 0.1f;
                    break;

                default:
                    break;
            } 
        }
        else
        {
            UIController.Instance.SetActiveNoticePopUp();
            Debug.Log("스킬 포인트 부족");
        }
    }


    private void UpdateUI()
    {
        levelText.text = upgradeData.level.ToString();
        costText.text = upgradeData.cost.ToString();    
    }
}
