using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public bool isFadeIn; // ���� �� ���̵��� ����
    public GameObject panel; // ���̵� �г�
    private CanvasRenderer panelRenderer; // CanvasRenderer ĳ��
    private Action onCompleteCallback; // ���̵� �Ϸ� �ݹ�

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // �� ��ȯ �Ŀ��� ����
        if (panel)
        {
            panelRenderer = panel.GetComponent<CanvasRenderer>();
        }
        else
        {
            throw new MissingComponentException();
        }
    }

    void Start()
    {
        if (isFadeIn)
        {
            panel.SetActive(true);
            StartCoroutine(CoFadeIn());
        }
        else
        {
            panel.SetActive(false);
        }
    }

    public void FadeOut()
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeOut());
    }

    public void FadeOutAndLoadScene(int sceneIndex)
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeOutAndLoadScene(sceneIndex));
    }

    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }

    private IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f;
        float fadeTime = 0.5f;

        while (elapsedTime <= fadeTime)
        {
            panelRenderer.SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.SetActive(false);
        onCompleteCallback?.Invoke();
    }

    private IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = 0.5f;

        while (elapsedTime <= fadeTime)
        {
            panelRenderer.SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCompleteCallback?.Invoke();
    }

    private IEnumerator CoFadeOutAndLoadScene(int sceneIndex)
    {
        float elapsedTime = 0f;
        float fadeTime = 0.5f;

        while (elapsedTime <= fadeTime)
        {
            panelRenderer.SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneIndex); // �� ��ȯ
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // �� ��ȯ �Ŀ��� �г� Ȱ��ȭ ����
        if (panel)
        {
            panel.SetActive(true);
            panelRenderer.SetAlpha(1f); // ���̵�ƿ� ���� ����
            StartCoroutine(CoFadeIn());
        }
    }
}
