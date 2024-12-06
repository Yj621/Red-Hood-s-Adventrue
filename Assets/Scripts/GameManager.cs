using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    public Player player;
    public ObjectPool ArrowPool;
    public PlayerController playerController;
    public GameObject CinemachineCamera;
         
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        instance = this;
        // player ������ �ʱ�ȭ
        player = new Player(100, 5, 0, 10);
        //PlayerSetting();
    }
   
    private void Start()
    {
        

    }

   /* public void PlayerSetting()
    {

        // PlayerController ���� �ʱ�ȭ
        if (playerController != null)
        {
            playerController.InitializePlayer();
        }

        // UIController�� �ʱ�ȭ�� ������ ��ٸ���
        StartCoroutine(WaitForUIController());
    }

    private IEnumerator WaitForUIController()
    {
        // UIController�� null�̸� ���
        while (UIController.Instance == null)
        {
            yield return null;  // �� ������ ���
        }

        // UI�� �غ�Ǹ� UI ������Ʈ
        UIController.Instance.UpdateCoinUI(player.Coins);
        UIController.Instance.UpdateExpUI(player.Experience, player.CalculateExperienceNextLevel(player.Level));
        UIController.Instance.UpdateLevelUI(player.Level);
        UIController.Instance.UpdateSkillPointUI(player.SkillPoints);
    }*/

    public void CameraOff()
    {
        if (CinemachineCamera == null)
        {
            // ��: �±׷� ã��
            CinemachineCamera = GameObject.FindWithTag("CinemachineCamera");

            // �±׷� �� ã�� ��� �̸����� ã��
            if (CinemachineCamera == null)
            {
                CinemachineCamera = GameObject.Find("CinemachineCamera");
            }

            // �׷��� �� ã���� ��� �α� ���
            if (CinemachineCamera == null)
            {
                Debug.LogWarning("CinemachineCamera�� ã�� �� �����ϴ�. ���� ������Ʈ�� Ȯ���ϼ���!");
            }
        }
        else
        {
            CinemachineCamera.SetActive(false);
        }
    }

    //  public void Restart()
    // {
    //     if(player.Hp >0)
    //     {
    //         CinemachineCamera.SetActive(true);
    //     }
    // }
}
