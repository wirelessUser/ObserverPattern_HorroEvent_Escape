using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
    SoundManager : MonoBehaviour
{
    public AudioSource audioEffects;
    public AudioSource backgroundMusic;
    public Sounds[] audioList;

    public static Action<SoundType, bool> OnPlaySoundEffects;

    private void OnEnable()
    {
        OnPlaySoundEffects += PlaySoundEffects;
        PlayerSanity.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        OnPlaySoundEffects -= PlaySoundEffects;
        PlayerSanity.OnPlayerDeath -= OnPlayerDeath;
    }

    private void Start()
    {
        PlayBackgroundMusic(SoundType.BackgroundMusic, true);
    }

    // Plays the given SoundType as Sound Effects.
    private void PlaySoundEffects(SoundType soundType, bool loopSound = false)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioEffects.loop = loopSound;
            audioEffects.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    // Plays the given SoundType as Background Music.
    private void PlayBackgroundMusic(SoundType soundType, bool loopSound = false)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            backgroundMusic.loop = loopSound;
            backgroundMusic.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    // Fetches the Sound Clip for the given SoundType.
    private AudioClip GetSoundClip(SoundType soundType)
    {
        Sounds st = Array.Find(audioList, item => item.soundType == soundType);
        if (st != null)
        {
            return st.audio;
        }
        return null;
    }

    // Sets the audio clip to null.
    public void StopSoundEffect()
    {
        audioEffects.Stop();
        audioEffects.clip = null;
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = null;
    }


    private void OnPlayerDeath()
    {
        Debug.Log("Player Died");
        OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
        PlayerSanity.OnPlayerDeath -= OnPlayerDeath;
    }

}


public enum SoundType
{
    BackgroundMusic,
    SwitchSound,
    DoorOpen,
    DoorSlam,
    SpookyGiggle,
    JumpScare1,
    JumpScare2,
    JumpScare3,
    KeyPickUp,
    DrinkPotion
}


[Serializable]
public class Sounds
{
    public SoundType soundType;
    public AudioClip audio;
}