using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundType { GameOver, GameClear, Jump, SerierCut, Cut, Bow, 
    BossAttack, BossWalk, BossHurt, BossDead, 
    PlayerWalk, PlayerSlide,
    BoarDead, SnailDead,
    PortalIn , PortalOut ,
    Coin, Exp, Potion, LevelUp}

    public AudioSource clickSound;
    
    [System.Serializable]
    public struct Sound
    {
        public SoundType type;
        public AudioSource audioSource;
    }
    public Sound[] sounds;
    private Dictionary<SoundType, AudioSource> soundDictionary;

    private static SoundManager instance;
    public static SoundManager Instance => instance;

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
        InitializeSoundDictionary();
    }

    public void ButtonSound()
    {
        clickSound.Play();
    }

    private void InitializeSoundDictionary()
    {
        soundDictionary = new Dictionary<SoundType, AudioSource>();
        foreach(var sound in sounds)
        {
            soundDictionary[sound.type] = sound.audioSource;
        }
    }
    public void PlaySound(SoundType soundType)
    {
        if(soundDictionary.TryGetValue(soundType, out AudioSource audioSource))
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log($"Sound {soundType} not found");
        }
    }

    public void StopSound(SoundType soundType)
    {
        if(soundDictionary.TryGetValue(soundType, out AudioSource audioSource))
        {
            audioSource.Stop();
        }
        else
        {
            Debug.Log($"Sound {soundType} not found");
        }
    }
}
