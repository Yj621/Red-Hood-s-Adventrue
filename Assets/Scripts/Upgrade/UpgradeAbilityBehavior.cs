using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeAbilityBehavior : MonoBehaviour, IUpgrade
{
    private Player player;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //���׷��̵�
    [SerializeField] private TextMeshProUGUI levelText; //����
    [SerializeField] private TextMeshProUGUI costText; //���
    public Abliity ability;

    private void Start()
    {
        //��ų ����Ʈ �ؽ�Ʈ�� �־��ֱ�
        levelText.text = ability.Level.ToString();
        //��ų ����Ʈ �ؽ�Ʈ�� �־��ֱ�
        costText.text = ability.Cost.ToString();
        player = PlayerController.Instance.player;
    }

    public void OnUpgradeButtonClick()
    {
        if (!ability.IsMaxLevel() && player.Coins - ability.Cost >= 0)
        {
            //���� / cost �ø���
            ability.UpLevel();
            UpdateUI();
            player.SetCoins(ability.Cost, "Down");
            ability.SetCost();


            switch (ability.UpgradeTitle)
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
        else
        {
            //UIController.Instance.SetActiveNoticePopUp();
            Debug.Log("���� ����");
        }
    }
   

    private void UpdateUI()
    {
        levelText.text = ability.Level.ToString();
        costText.text = ability.Cost.ToString();    
    }

}
