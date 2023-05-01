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


    private void Start()
    {
        playerController = new PlayerController(playerView, playerScriptableObject);
    }

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
}
