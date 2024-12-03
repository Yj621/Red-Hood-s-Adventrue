using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class NoticePopUp : MonoBehaviour
{

    private static NoticePopUp instance;
    public static NoticePopUp Instance
    { get { return instance; }}

    void Start()
    {
        instance = this;
    }
    void OnEnable()
    {
        //여기서 coin이 부족한지 skillpoint가 부족한지 판단해서 noticeText를 바꿔서 출력ㅎ줌
    }
}
