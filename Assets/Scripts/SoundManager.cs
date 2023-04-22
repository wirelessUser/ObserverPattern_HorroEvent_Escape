using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericMonoSingleton<SoundManager>
{
    public AudioSource audioEffects;
    public AudioSource backgroundMusic;
    public Sounds[] audioList;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PlayBackgroundMusic(SoundType.BackgroundMusic, true);
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