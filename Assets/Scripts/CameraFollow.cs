using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineCamera Camera;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        Camera = GetComponent<CinemachineCamera>();
        Camera.Follow = player.transform;
    }

}
