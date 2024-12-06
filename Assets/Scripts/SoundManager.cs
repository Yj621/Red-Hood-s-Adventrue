using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource gameOver;
    public AudioSource gameClear;
    public AudioSource jump;
    public AudioSource seriesCut;
    public AudioSource cut;

    private static SoundManager instance;
    public static SoundManager Instance
    { get { return instance; }}

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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
