using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    private PlayerController playerController;

    [Header("Views")]
    [SerializeField] private PlayerView playerView;
    [SerializeField] private SoundView soundView;
    [SerializeField] private GameUIView gameUIView;
    [SerializeField] private InstructionView instructionView;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    private void Start() => playerController = new PlayerController(playerView, playerScriptableObject);

    //Todo - Ask Mayank -> Converting all these into properties as {get;private set;} is good or 
    // we can keep it same for now?

    public PlayerController GetPlayerController()
    {
        return playerController;
    }
    public GameUIView GetGameUI()
    {
        return gameUIView;
    }
    public InstructionView GetInstructionView()
    {
        return instructionView;
    }
    public SoundView GetSoundView()
    {
        return soundView;
    }
    public void GameOver()
    {
        playerController.KillPlayer();
        soundView.PlaySoundEffects(SoundType.JumpScare1);
    }
}
