using UnityEngine;

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
        //player 데이터 초기화 (Hp, Damage, Exp, Coins)
        player = new Player(100, 5, 0, 10);

    }
    
    void Start()
    {
    }



    void Update()
    {

    }

    public void CameraOff()
    {
        CinemachineCamera.SetActive(false); 
    }

    //  public void Restart()
    // {
    //     if(player.Hp >0)
    //     {
    //         CinemachineCamera.SetActive(true);
    //     }
    // }
}
