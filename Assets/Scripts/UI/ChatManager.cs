using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    private Queue<string> messageQueue = new Queue<string>(); //메세지 저장 큐
    public int maxMessage = 10;
    public float fadeDuration = 2f;
    public CanvasGroup canvasGroup;

    private static ChatManager instance;
    public static ChatManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        /*        if (Instance != this && Instance != null)
                {
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    instance = this;
                    DontDestroyOnLoad(gameObject);
                }*/
    }
    void Start()
    {
        instance = this;
    }

    public void AddMessage(string message)
    {
        messageQueue.Enqueue(message);
        if (messageQueue.Count > maxMessage)
        {
            messageQueue.Dequeue();
        }
        UpdateChatText();
        StopAllCoroutines(); // 기존의 투명화 코루틴이 실행 중일 경우 중지
        StartCoroutine(FadeOutPanel());
    }

    private void UpdateChatText()
    {
        Dictionary<string, string> keywordColors = new Dictionary<string, string>()
        {
            {"Coin", "yellow"},
            {"Exp", "green"},
            {"Potion", "red"}
        };
        string[] processedMessage = messageQueue.ToArray();
        for (int i = 0; i < processedMessage.Length; i++)
        {
            foreach (var keyword in keywordColors)
            {
                processedMessage[i] = processedMessage[i].Replace(
                    keyword.Key,
                    $"<color={keyword.Value}>{keyword.Key}</color>");
            }

        }
        // 처리된 메세지를 TextMeshPro에 표시
        chatText.text = string.Join("\n", processedMessage);
    }

    private IEnumerator FadeOutPanel()
    {
        // canvasGroup이 null인 경우 동적으로 할당
        if (canvasGroup == null)
        {
            GameObject canvasGroupObject = GameObject.Find("Chatting_Panel"); // CanvasGroup이 붙어 있는 GameObject 이름
            if (canvasGroupObject != null)
            {
                canvasGroup = canvasGroupObject.GetComponent<CanvasGroup>();
            }
        }

        // canvasGroup을 찾지 못했다면 종료
        if (canvasGroup == null) yield break;

        canvasGroup.alpha = 1;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }


}
