using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource m_AudioEffects;
    public AudioSource m_BackgroundMusic;

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
            m_AudioEffects.loop = loopSound;
            m_AudioEffects.PlayOneShot(clip);
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
            m_BackgroundMusic.loop = loopSound;
            m_BackgroundMusic.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    // Fetches the Sound Clip for the given SoundType.
    private AudioClip GetSoundClip(SoundType soundType)
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
        m_AudioEffects.Stop();
        m_AudioEffects.clip = null;
    }

    public void StopBackgroundMusic()
    {
        m_BackgroundMusic.Stop();
        m_BackgroundMusic.clip = null;
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
}


[Serializable]
public class Sounds
{
    public SoundType soundType;
    public AudioClip audio;
}