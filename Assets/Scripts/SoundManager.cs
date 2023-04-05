using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioEffects;
    public AudioSource BackgroundMusic;

    public Sounds[] AudioList;

    public static Action<SoundType, bool> OnPlaySoundEffects;

    private void OnEnable()
    {
        OnPlaySoundEffects += PlaySoundEffects;
    }

    private void OnDisable()
    {
        OnPlaySoundEffects -= PlaySoundEffects;
    }

    // Plays the given SoundType as Sound Effects.
    public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
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
    public void PlayBackgroundMusic(SoundType soundType, bool loopSound = false)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            BackgroundMusic.loop = loopSound;
            BackgroundMusic.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    // Fetches the Sound Clip for the given SoundType.
    public AudioClip GetSoundClip(SoundType soundType)
    {
        Sounds st = Array.Find(AudioList, item => item.soundType == soundType);
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

}


public enum SoundType
{
    BackgroundMusic,
    SwitchSound,
    PlayerDeath,
    GameOver,
    Jump,
    Land,
    Giggle,
    ScarySound1,
    ScarySound2,
    ScarySound3,
    DoorSlam
}


[Serializable]
public class Sounds
{
    public SoundType soundType;
    public AudioClip audio;
}