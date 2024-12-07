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
            // SettingPanel�� Ȱ��ȭ ���¸� ���
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
