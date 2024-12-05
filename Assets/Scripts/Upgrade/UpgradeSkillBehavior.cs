using TMPro;
using UnityEngine;

public class UpgradeSkillBehavior : MonoBehaviour, IUpgrade
{
    private Player player;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //업그레이드
    [SerializeField] private TextMeshProUGUI skillPointLevelText; //레벨
    public UpgradeLevel EnhanceInfo;

    private void Start()
    {
        //스킬 포인트 텍스트에 넣어주기
        skillPointLevelText.text =  EnhanceInfo.Level.ToString();
        player = GameManager.Instance.player;
    }

    public void OnUpgradeButtonClick()
    {
        if (!EnhanceInfo.IsMaxLevel()&& player.SkillPoints > 0)
        {
            //레벨 / cost 올리기
            EnhanceInfo.UpLevel();
            UpdateUI();
            player.SetSkillPoints(1, "Down");


            switch (EnhanceInfo.UpgradeTitle)
            {
                case "활 쏘기":
                    //활쏘기 ++
                    player.weapon.bowDamage += 1f;
                    Debug.Log(player.weapon.bowDamage);
                    break;

                case "연속 베기":
                    //연속 베기 ++ 
                    player.weapon.seriesCutDamage += 1f;
                    break;

                case "공격 속도":
                    //모든 스킬 공격 속도
                    Debug.Log("공격속도 업그레이드");
                    AnimationSpeed.Instance.PlusAnimationSpeed();
                    break;

                case "쿨타임 감소":
                    //모든 스킬 쿨타임 감소
                    Debug.Log("쿨타임 감소 업그레이드");
                    break;

                default:
                    break;
            }
        }
        else
        {
            UIController.Instance.SetActiveNoticePopUp();
            UIController.Instance.noticeText.text = "스킬포인트가 부족합니다.";
            Debug.Log("스킬포인트 부족");
        }
    }


    private void UpdateUI()
    {
        skillPointLevelText.text = EnhanceInfo.Level.ToString(); 
    }


}
