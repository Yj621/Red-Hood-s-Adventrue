using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public bool isFadeIn; // 시작 시 페이드인 여부
    public GameObject panel; // 페이드 패널
    private CanvasRenderer panelRenderer; // CanvasRenderer 캐시
    private Action onCompleteCallback; // 페이드 완료 콜백

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // 씬 전환 후에도 유지
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
        SceneManager.LoadScene(sceneIndex); // 씬 전환
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // 씬 전환 후에도 패널 활성화 유지
        if (panel)
        {
            panel.SetActive(true);
            panelRenderer.SetAlpha(1f); // 페이드아웃 상태 유지
            StartCoroutine(CoFadeIn());
        }
    }
}
