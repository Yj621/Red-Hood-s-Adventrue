using TMPro;
using UnityEngine;

public class UpgradeSkillBehavior : MonoBehaviour, IUpgrade
{
    private Player player;

    [SerializeField] private TextMeshProUGUI upgradeTitleText; //���׷��̵�
    [SerializeField] private TextMeshProUGUI skillPointLevelText; //����
    public UpgradeLevel EnhanceInfo;

    private void Start()
    {
        //��ų ����Ʈ �ؽ�Ʈ�� �־��ֱ�
        skillPointLevelText.text =  EnhanceInfo.Level.ToString();
        player = PlayerController.Instance.player;
    }

    public void OnUpgradeButtonClick()
    {
        if (!EnhanceInfo.IsMaxLevel())
        {
            //���� / cost �ø���
            EnhanceInfo.UpLevel();
            UpdateUI();
            player.SetSkillPoints(1, "Down");


            switch (EnhanceInfo.UpgradeTitle)
            {
                case "����":
                    //���� ��ų ++
                    Debug.Log("���� ��ų ���׷��̵�");
                    break;

                case "Ȱ ���":
                    //Ȱ��� ++
                    Debug.Log("Ȱ��� ��ų ���׷��̵�");
                    break;

                case "���� ����":
                    //���� ���� ++ 
                    Debug.Log("���� ���� ��ų ���׷��̵�");
                    break;

                case "���� �ӵ�":
                    //��� ��ų ���� �ӵ�
                    Debug.Log("���ݼӵ� ���׷��̵�");
                    break;

                case "��Ÿ�� ����":
                    //��� ��ų ��Ÿ�� ����
                    Debug.Log("��Ÿ�� ���� ���׷��̵�");
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
        skillPointLevelText.text = EnhanceInfo.Level.ToString(); 
    }


}
