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
        // player 데이터 초기화
        player = new Player(100, 5, 0, 10);
        //PlayerSetting();
    }
   
    private void Start()
    {
        

    }

   /* public void PlayerSetting()
    {

        // PlayerController 상태 초기화
        if (playerController != null)
        {
            playerController.InitializePlayer();
        }

        // UIController가 초기화될 때까지 기다리기
        StartCoroutine(WaitForUIController());
    }

    private IEnumerator WaitForUIController()
    {
        // UIController가 null이면 대기
        while (UIController.Instance == null)
        {
            yield return null;  // 한 프레임 대기
        }

        // UI가 준비되면 UI 업데이트
        UIController.Instance.UpdateCoinUI(player.Coins);
        UIController.Instance.UpdateExpUI(player.Experience, player.CalculateExperienceNextLevel(player.Level));
        UIController.Instance.UpdateLevelUI(player.Level);
        UIController.Instance.UpdateSkillPointUI(player.SkillPoints);
    }*/

    public void CameraOff()
    {
        if (CinemachineCamera == null)
        {
            // 예: 태그로 찾기
            CinemachineCamera = GameObject.FindWithTag("CinemachineCamera");

            // 태그로 못 찾는 경우 이름으로 찾기
            if (CinemachineCamera == null)
            {
                CinemachineCamera = GameObject.Find("CinemachineCamera");
            }

            // 그래도 못 찾으면 경고 로그 출력
            if (CinemachineCamera == null)
            {
                Debug.LogWarning("CinemachineCamera를 찾을 수 없습니다. 게임 오브젝트를 확인하세요!");
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
