using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioSource audioEffects; 
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Sounds[] audioList;

    private void Start()
    {
        PlayBackgroundMusic(SoundType.BackgroundMusic, true);
    }

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


    public void PlayBackgroundMusic(SoundType soundType, bool loopSound = false)
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


    private AudioClip GetSoundClip(SoundType soundType)
    {
        Sounds st = Array.Find(audioList, item => item.soundType == soundType);
        if (st != null)
        {
            return st.audio;
        }
        return null;
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