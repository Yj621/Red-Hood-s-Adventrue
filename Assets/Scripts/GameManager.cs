using UnityEngine;

public class GameManager : MonoBehaviour
{    
    private Player player;
    public ObjectPool ArrowPool;
    public GameObject CinemachineCamera;
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        instance = this;
        player = PlayerController.Instance.player;
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
