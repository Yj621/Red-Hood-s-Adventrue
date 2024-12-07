using UnityEngine;
using UnityEngine.EventSystems;

public class Setting : MonoBehaviour
{
    public GameObject canvas;
    private void Awake()
    {
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // SettingPanel의 활성화 상태를 토글
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
